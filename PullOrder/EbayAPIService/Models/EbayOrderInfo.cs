using System;

namespace DataBase.Models
{
    public class EbayOrderInfo :IEbayOrderMappable
    {
        public ApiUserPlatformToken apiUserPlatformToken { get; private set; }
        public EbayOrderResult ebayOrderResult { get; private set; }

        public EbayOrderInfo(ApiUserPlatformToken apiUserPlatformToken, EbayOrderResult ebayOrderResult)
        {
            this.apiUserPlatformToken = apiUserPlatformToken;
            this.ebayOrderResult = ebayOrderResult;
        }

        public string GetApiId()
        {
            return apiUserPlatformToken.ApiId;
        }

        public string GetApiUserId()
        {
            return apiUserPlatformToken.ApiUserId;
        }

        public long GetApiUserPlatformTokenId()
        {
            return apiUserPlatformToken.ApiUserPlatformTokenId;
        }

        public string GetOrderId()
        {
            return ebayOrderResult.orderId;
        }

        public string GetOrderStatus()
        {
            return ebayOrderResult.orderStatus;
        }

        public decimal GetOrderSubtotal()
        {
            return Convert.ToDecimal(ebayOrderResult.orderSubTotal);
        }

        public decimal GetOrderTotal()
        {
            return Convert.ToDecimal(ebayOrderResult.orderTotal);
        }

        public DateTime GetOrderCreateAt()
        {
            return ebayOrderResult.orderCreateAt;
        }

        public string GetShippingName()
        {
            return ebayOrderResult.shippingName;
        }

        public string GetShippingStreet()
        {
            return ebayOrderResult.shippingStreet;
        }

        public string GetShippingStreet1()
        {
            return ebayOrderResult.shippingStreet1;
        }

        public string GetShippingStreet2()
        {
            return ebayOrderResult.shippingStreet2;
        }

        public string GetShippingCityName()
        {
            return ebayOrderResult.shippingCityName;
        }

        public string GetShippingState()
        {
            return ebayOrderResult.shippingState;
        }

        public string GetShippingCountry()
        {
            return ebayOrderResult.shippingCountry;
        }

        public string GetShippingCountryName()
        {
            return ebayOrderResult.shippingCountryName;
        }

        public string GetShippingPhone()
        {
            return ebayOrderResult.shippingPhone;
        }

        public string GetShippingPostalCode()
        {
            return ebayOrderResult.shippingPostalCode;
        }

        public string GetShippingAddressId()
        {
            return ebayOrderResult.shippingAddressId;
        }

        public string GetShippingService()
        {
            return ebayOrderResult.shippingService;
        }

        public int GetShippingTimeMin()
        {
            return ebayOrderResult.shippingTimeMin;
        }

        public int GetShippingTimeMax()
        {
            return ebayOrderResult.shippingTimeMax;
        }

        public string GetBuyerEbayUserId()
        {
            return ebayOrderResult.buyerUserid;
        }

        public DateTime? GetPaymentTime()
        {
            return ebayOrderResult.paymentTime;
        }
    }

}
