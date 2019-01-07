using DataBase.Models;
using PullOrderTransaction.Base.Models;
using PullOrderTransaction.Models;
using System;
using System.Threading.Tasks;
using PullOrderTransaction.Base;
using System.Threading;

namespace PullOrderTransaction
{
    public class PullOrderTransactionProcess : PullOrderTransactionBase
    {
        public async Task<QueuePullOrder> Process(QueuePullOrder payload)
        {
            QueuePullOrder returnpayload = new QueuePullOrder(); 
            PullOrderLog = new LogPullOrder()
            {
                ProcessStartTime = DateTime.UtcNow,
                ProcessStatus = (int)EnumLogStatus.Pending,
                ApiUserPlatformTokenId = payload.ApiUserPlatformTokenId
            };
            try
            {
                PullOrdersTranSactionWorking PullOrdersWorker = new PullOrdersTranSactionWorking(payload)
                { workinglog = PullOrderLog };
               PullOrderLog = await PullOrdersWorker.Working();
                returnpayload = PullOrdersWorker.ReturnPayload;
            }
            catch (Exception ex)
            {
                this.SetFailLog(ex.ToString());
            }
            this.SaveLogToDB();

            return returnpayload;
        }
    }
}
