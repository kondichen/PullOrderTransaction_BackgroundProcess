using DataBase;
using DataBase.Models;
using PullOrderTransaction.Base;
using PullOrderTransaction.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PullOrderTransaction
{
    public class PullOrdersTranSactionWorking : PullOrderTransactionBase
    {
        private List<EbayOrderTransaction> orderTransactions;
        private ApiUserPlatformToken apiUserPlatformToken;
        private readonly QueuePullOrder PayLoad;
        public QueuePullOrder ReturnPayload { get; set; }
        private EbayOrderResult EbayOrderResult;
        public LogPullOrder workinglog;
        private EbayOrderInfo ebayOrderInfo;
        private EbayOrder ebayOrder;
        public PullOrdersTranSactionWorking( QueuePullOrder payload)
        {    
            this.PayLoad = payload;
        }

        public async Task<LogPullOrder> Working()
        {
            this.PullOrderLog = workinglog;
            try
            {
                PullOrderLog = await GetOrdersTranSactionFromEBayAsync();
                if (PullOrderLog.ProcessStatus == (int)EnumLogStatus.Fail)
                    return PullOrderLog;

                UpdateDataBase();
                PullOrderLog.ProcessEndTime = DateTime.UtcNow;
                PullOrderLog.ProcessMessage = "PullOrder Transaction BGP Success : " + ebayOrder.OrderId + "Order and " + orderTransactions.Count.ToString() + "it's Transactions Updated";
                PullOrderLog.ProcessStatus = (int)EnumLogStatus.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(PayLoad.OrderId);
                this.SetFailLog(PayLoad.OrderId + "   " + ex.ToString());
            }
            return PullOrderLog;
        }

        private async Task<LogPullOrder> GetOrdersTranSactionFromEBayAsync()
        {
            try
            {
                string error = string.Empty;
                using (mardevContext context = new mardevContext())
                {
                    apiUserPlatformToken = context.ApiUserPlatformToken
                        .Where(x => x.ApiUserPlatformTokenId == PayLoad.ApiUserPlatformTokenId)
                        .FirstOrDefault();
                };

                if (apiUserPlatformToken == null)
                {
                    error = PayLoad.ApiUserPlatformTokenId.ToString() + " is not exist in Database";
                    return SetFailLog(error);
                }
                if (string.IsNullOrWhiteSpace(apiUserPlatformToken.AccessToken))
                {
                    error = PayLoad.ApiUserPlatformTokenId.ToString() + " hasn't AccessToken in ApiUserPlatformToken";
                    return SetFailLog(error);
                }


                using (GetOrdersTranSactionService service = new GetOrdersTranSactionService(apiUserPlatformToken))
                {
                    EbayOrderTransactionRequest ebayOrderRequest = new EbayOrderTransactionRequest()
                    {
                        OrderID = PayLoad.OrderId,
                    };
                    service.EbayApilog = PullOrderLog;
                    EbayOrderResult = await service.GetOrdersTranSactionAsync(ebayOrderRequest);

                    ebayOrderInfo = new EbayOrderInfo(apiUserPlatformToken, EbayOrderResult);
                }
            }
            catch (Exception ex)
            {
                SetFailLog(PayLoad.OrderId+" Error :" + ex.ToString());
            }

            return PullOrderLog;
        }

        private void UpdateDataBase()
        {
            using (mardevContext context = new mardevContext())
            {
                UpdateEbayOrder(context);
                UpdateEbayOrderTransaction(context);
                context.SaveChanges();
            }
            ReturnPayload = this.PayLoad;
            ReturnPayload.OrderId = ebayOrder.MarketPlaceOrderNumber;
        }

        private void UpdateEbayOrder(mardevContext context)
        {
            ebayOrder = context.EbayOrder.Where(x =>
                 x.ApiId == apiUserPlatformToken.ApiId &&
                 x.ApiUserId == apiUserPlatformToken.ApiUserId &&
                x.OrderId == PayLoad.OrderId
                ).FirstOrDefault();

            if (ebayOrder == null)
            {
                ebayOrder = EbayOrder.Create(ebayOrderInfo);
                string Seq = context.p_GetNextOrderNumberSeq(PayLoad.PullOrderQueueId).ToString();
                ebayOrder.MarketPlaceOrderNumber = EbayOrder.BuildOrderNo(Seq);
            }
            else
            {
                EbayOrder.Update(ebayOrder, ebayOrderInfo);
            }

            if (ebayOrder.EbayOrderId == Guid.Empty)
            {
                ebayOrder.EbayOrderId = Guid.NewGuid();
                ebayOrder.CreateOn = DateTime.UtcNow;
                context.EbayOrder.Add(ebayOrder);
            }
            else
                context.EbayOrder.Update(ebayOrder);

            ebayOrder.ModifyOn = DateTime.UtcNow;
        }

        private void UpdateEbayOrderTransaction(mardevContext context)
        {
            GetEbayOrderTransaction(context);
            foreach (EbayOrderTransaction ebayOrderTransaction in orderTransactions)
            {
                if (ebayOrderTransaction.EbayOrderTransactionId == Guid.Empty)
                {
                    ebayOrderTransaction.EbayOrderTransactionId = Guid.NewGuid();
                    ebayOrderTransaction.CreateOn = DateTime.UtcNow;
                    context.EbayOrderTransaction.Add(ebayOrderTransaction);
                }
                else
                    context.EbayOrderTransaction.Update(ebayOrderTransaction);

                ebayOrderTransaction.ModifyOn = DateTime.UtcNow;
            }
        }

        private void GetEbayOrderTransaction(mardevContext context)
        {
            orderTransactions = new List<EbayOrderTransaction>();
            long tranSeq = RetrieveSystemTransactionNo(context);

            foreach (EbayOrderTransactionResponse ebayOrderTransactionResponse in EbayOrderResult.orderTransactions)
            {
                EbayOrderTransaction ebayOrderTransaction = context.EbayOrderTransaction.Where(x =>
                 x.ApiId == apiUserPlatformToken.ApiId &&
                 x.ApiUserId == apiUserPlatformToken.ApiUserId &&
                 x.ItemId == ebayOrderTransactionResponse.itemId &&
                 x.TransactionId == ebayOrderTransactionResponse.transactionId).FirstOrDefault();

                EbayOrderTransactionInfo info = new EbayOrderTransactionInfo(ebayOrder, ebayOrderTransactionResponse);
                if (ebayOrderTransaction == null)
                {
                    ebayOrderTransaction = EbayOrderTransaction.Create(info);
                    ebayOrderTransaction.MarketPlaceTransactionNumber = EbayOrderTransaction.BuildSystemTransactionNo(tranSeq);
                    ebayOrderTransaction.ItemSite = PayLoad.SiteCode;
                    tranSeq++;
                }
                else
                {
                    EbayOrderTransaction.Update(ebayOrderTransaction, info);
                }
                orderTransactions.Add(ebayOrderTransaction);
            }

        }

        private long RetrieveSystemTransactionNo(mardevContext context)
        {
            int existingTransCount = 0;

            foreach (EbayOrderTransactionResponse info in EbayOrderResult.orderTransactions)
            {
                EbayOrderTransaction transaction = context.EbayOrderTransaction.Where(x =>
                 x.ApiId == apiUserPlatformToken.ApiId &&
                 x.ApiUserId == apiUserPlatformToken.ApiUserId &&
                 x.ItemId == info.itemId &&
                 x.TransactionId == info.transactionId).FirstOrDefault();

                if (transaction != null)
                {
                    existingTransCount++;
                }
            }

            int numberOfSeqRetrieve = EbayOrderResult.orderTransactions.Count - existingTransCount;
            if (numberOfSeqRetrieve > 0)
            {
                return context.RetrieveBatchTransactionSeq(PayLoad.PullOrderQueueId);
            }
            else
            {
                return numberOfSeqRetrieve;
            }
        }

    }
}
