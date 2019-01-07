using DataBase;
using DataBase.Models;
using Newtonsoft.Json;
using PullOrderTransaction.Base;
using PullOrderTranSaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace PullOrderTransaction
{
    public class PullOrderTransactionStart : PullOrderTransactionBase
    {
        private class NotificationData
        {
            public int NotficationType { get; set; }
            public string UserName { get; set; }
            public string EbayUserName { get; set; }
            public List<string> MarketPlaceList { get; set; }
        }
        private class CheckOrderFinished
        {
            public int OrdersCount { get; set; }
            public string CallBackUrl { get; set; }
            public string EbayUserName { get; set; }
            public string UserName { get; set; }
            public long ApiUserPlatFormTokenID { get; set; }
            public Guid FromID { get; set; }
            public List<string> OrderNumList { get; set; }
            public bool IsFinished { get; set; }
        }
        private List<Task<QueuePullOrder>> TaskList;
        private List<CheckOrderFinished> NotificationOrderList;
        private int MaxThreadCount;
        public PullOrderTransactionStart()
        {
            this.AppSettings = SetAppSettings();
        }

        public void Start()
        {
            TaskList = new List<Task<QueuePullOrder>>();
            NotificationOrderList = new List<CheckOrderFinished>();
            MaxThreadCount = AppSettings.MaxThreadCount;
            while (true)
            {
                try
                {
                    CheckTaskIsCompleted();

                    if (MaxThreadCount == 0)
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                    QueuePullOrder PullOrder = new QueuePullOrder();
                    using (mardevContext context = new mardevContext())
                    {
                        PullOrder = context.QueuePullOrder.Where(x => !x.IsUsed).FirstOrDefault();
                        if (PullOrder != null)
                        {

                            PullOrder.IsUsed = true;
                            context.QueuePullOrder.Update(PullOrder);
                            context.SaveChanges();


                            if (!NotificationOrderList.Where(x => x.ApiUserPlatFormTokenID == PullOrder.ApiUserPlatformTokenId && x.FromID == PullOrder.PullOrderFromID).Any())
                            {
                                int QueueTotal = context.QueuePullOrder
                                                 .Where(x => x.ApiUserPlatformTokenId == PullOrder.ApiUserPlatformTokenId &&
                                                        x.PullOrderFromID == PullOrder.PullOrderFromID).Count();

                                var query = (from a in context.ApiUserPlatformToken
                                             where a.ApiUserPlatformTokenId == PullOrder.ApiUserPlatformTokenId
                                             join b in context.ApiUser on a.ApiUserId equals b.ApiUserId
                                             select new
                                             {
                                                 a.PlatformUsername,
                                                 b.CallBackUrl,
                                                 b.Username
                                             }).FirstOrDefault();

                                NotificationOrderList.Add(new CheckOrderFinished()
                                {
                                    OrdersCount = QueueTotal,
                                    ApiUserPlatFormTokenID = PullOrder.ApiUserPlatformTokenId,
                                    FromID = PullOrder.PullOrderFromID,
                                    OrderNumList = new List<string>(),
                                    CallBackUrl = query != null ? query.CallBackUrl : "",
                                    UserName = query != null ? query.Username : "",
                                    EbayUserName = query != null ? query.PlatformUsername : "",
                                    IsFinished = false
                                });
                            }
                        }
                    };



                    if (PullOrder == null)
                    {
                        Thread.Sleep(10000);
                        continue;
                    }

                    PullOrderTransactionProcess DoPcc = new PullOrderTransactionProcess() { };
                    Task<QueuePullOrder> NewTask = Task.Run(async () => { return await DoPcc.Process(PullOrder); });
                    //統計Task列表
                    TaskList.Add(NewTask);
                    MaxThreadCount--;
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void CheckTaskIsCompleted()
        {
            if (TaskList.Count != 0)
            {
                //Task 在Running狀態無法跑foreach
                var GetNotRunList = TaskList.Where(x => x.Status != TaskStatus.Running).ToList();

                foreach (Task<QueuePullOrder> t in GetNotRunList)
                {
                    if (t.IsCompleted && t.Result != null)
                    {
                        NotificationOrderList = (NotificationOrderList.Where(x => x.ApiUserPlatFormTokenID == t.Result.ApiUserPlatformTokenId &&
                                                          x.FromID == t.Result.PullOrderFromID)
                                                         .Select(x =>
                                                         {
                                                             x.OrderNumList.Add(t.Result.OrderId);
                                                             x.OrdersCount--;
                                                             return x;

                                                         })).ToList();


                        TaskList.Remove(t);
                        MaxThreadCount++;

                        var FinishedOrdersList = NotificationOrderList.Where(x => x.OrdersCount == 0).ToList() ;
                        if (FinishedOrdersList != null && FinishedOrdersList.Count() > 0)
                        {
                            CheckOrderFinished removeitem = new CheckOrderFinished();
                            foreach (CheckOrderFinished DoNotificationOder in FinishedOrdersList)
                            {
                                removeitem = DoNotificationOder;
                                NotificationOrderList.Remove(removeitem);
                                SendOrderFinishedNotificationAsync(DoNotificationOder);
                                
                            }
                        }
                    }
                }
            }
        }

        private async Task SendOrderFinishedNotificationAsync(CheckOrderFinished NotificationData)
        {
            string TargetUrl = NotificationData.CallBackUrl;
            NotificationData data = new NotificationData()
            {
                NotficationType = (int)EnumNotificationType.PullOrderTransactionNotification,
                EbayUserName = NotificationData.EbayUserName,
                UserName = NotificationData.UserName,
                MarketPlaceList = NotificationData.OrderNumList
            };
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = null;

                    // Content-Type 用於宣告遞送給對方的文件型態
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                    var JSONdata = JsonConvert.SerializeObject(data);
                    using (var Content = new StringContent(JSONdata, Encoding.UTF8, "application/json"))
                    {
                        response = await client.PostAsync(TargetUrl, Content);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
