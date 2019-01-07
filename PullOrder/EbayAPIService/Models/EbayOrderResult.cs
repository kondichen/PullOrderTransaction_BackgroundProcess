using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Models
{
   public class EbayOrderResult
    {
        public string orderId;
        public string orderStatus;
        public double orderSubTotal;
        public double orderTotal;
        public DateTime orderCreateAt;
        public string shippingName;
        public string shippingStreet;
        public string shippingStreet1;
        public string shippingStreet2;
        public string shippingCityName;
        public string shippingState;
        public string shippingCountry;
        public string shippingCountryName;
        public string shippingPhone;
        public string shippingPostalCode;
        public string shippingAddressId;
        public string shippingService;
        public int shippingTimeMin;
        public int shippingTimeMax;
        public string buyerUserid;
        public DateTime? paymentTime;
        public DateTime? shippedTime;
        public string shippingCarrierUsed;
        public string shipmentTrackingNumber;

        public List<EbayOrderTransactionResponse> orderTransactions;
        public List<string> OrderTransIdList;
    }
}
