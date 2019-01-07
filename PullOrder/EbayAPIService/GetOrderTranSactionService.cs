using DataBase.Models;
using PullOrderTransaction.Base;
using PullOrderTransaction.Base.Models;
using ServiceReference1;
using System;
using System.Threading.Tasks;
using static ServiceReference1.eBayAPIInterfaceClient;

namespace DataBase
{
    public class GetOrdersTranSactionService : PullOrderTransactionBase, IDisposable
    {

        private ApiUserPlatformToken ApiUser;
        private eBayAPIInterfaceClient service;
        private EbayOrderResult EbayOrderResult;
        public LogPullOrder EbayApilog;
        public GetOrdersTranSactionService(ApiUserPlatformToken apiuser)
        {
           this.EbayAPISettings  = SetEbayAPIConfig();
            this.ApiUser = apiuser;
            string requestURL = "https://api.ebay.com/wsapi"
              + "?callname=" + "GetOrderTransactions"
              + "&siteid=" + EbayAPISettings.SiteID
              + "&appid=" + EbayAPISettings.AppID
              + "&version=" + EbayAPISettings.Version
              + "&routing=new";
            service = new eBayAPIInterfaceClient(EndpointConfiguration.eBayAPI, requestURL);
        }

        public async Task<EbayOrderResult> GetOrdersTranSactionAsync(EbayOrderTransactionRequest orderrequest)
        {
            try
            {
                PullOrderLog = EbayApilog;
                GetOrderTransactionsRequestType request = new GetOrderTransactionsRequestType()
                {
                    OrderIDArray = new string[] { orderrequest.OrderID },
                    DetailLevel = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnAll },
                    Version = EbayAPISettings.Version,
                };
                CustomSecurityHeaderType requesterCredentials = new CustomSecurityHeaderType
                {
                    eBayAuthToken = ApiUser.AccessToken,
                    Credentials = new UserIdPasswordType()
                    {
                        AppId = EbayAPISettings.AppID,
                        DevId = EbayAPISettings.DevID,
                        AuthCert = EbayAPISettings.AuthCert,
                    },
                    
                    
                };
                OrderType[] response = (await service.GetOrderTransactionsAsync(requesterCredentials, request)).GetOrderTransactionsResponse1.OrderArray;
                EbayOrderResponseBuilder builder = new EbayOrderResponseBuilder(response[0]);
                EbayOrderResult = builder.ToResponse();
            }
            catch (Exception ex)
            {
                SetFailLog(ex.ToString());
            }
            return EbayOrderResult;
        }
        public void Dispose()
        {
        }
    }
}
