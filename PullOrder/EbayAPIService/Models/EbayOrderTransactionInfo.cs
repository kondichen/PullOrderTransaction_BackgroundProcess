using System;

namespace DataBase.Models
{
    public class EbayOrderTransactionInfo : IEbayOrderTransactionMappable
    {
        public EbayOrder ebayOrder { get; protected set; }
        public EbayOrderTransactionResponse orderTransactionResponse { get; protected set; }

        public EbayOrderTransactionInfo(EbayOrder ebayOrder, EbayOrderTransactionResponse orderTransactionResponse)
        {
            this.ebayOrder = ebayOrder;
            this.orderTransactionResponse = orderTransactionResponse;
        }

        public string GetApiId()
        {
            return ebayOrder.ApiId;
        }
        public string GetApiUserId()
        {
            return ebayOrder.ApiUserId;
        }
        public long GetApiUserPlatformTokenId()
        {
            return ebayOrder.ApiUserPlatformTokenId;
        }
        public Guid GetEbayOrderId()
        {
            return ebayOrder.EbayOrderId;
        }
        public string GetBuyerUserId()
        {
            return ebayOrder.BuyerEbayUserId;
        }
        public string GetBuyerEmail()
        {
            return orderTransactionResponse.buyerEmail;
        }
        public string GetItemId()
        {
            return orderTransactionResponse.itemId;
        }
        public string GetListingType()
        {
            return orderTransactionResponse.listingType;
        }
        public int GetQuantityPurchase()
        {
            return orderTransactionResponse.quantityPurchase;
        }
        public string GetTransactionId()
        {
            return orderTransactionResponse.transactionId;
        }
        public decimal GetTransactionPrice()
        {
            return Convert.ToDecimal(orderTransactionResponse.transactionPrice);
        }
        public string GetItemSku()
        {
            return orderTransactionResponse.itemSku;
        }
        public string GetVariationSku()
        {
            return orderTransactionResponse.variationSku;
        }
        public string GetPlatform()
        {
            return orderTransactionResponse.platform;
        }
        public string GetTransactionSiteId()
        {
            return orderTransactionResponse.transactionSiteId;
        }

        public DateTime? GetShippedTime()
        {
            return orderTransactionResponse.shippedTime;
        }

        public string GetShippingCarrierUsed()
        {
            return orderTransactionResponse.shippingCarrierUsed;
        }

        public string GetShipmentTrackingNumber()
        {
            return orderTransactionResponse.shipmentTrackingNumber;
        }

        public bool GetIsShipped()
        {
            return GetShippedTime().HasValue;
        }
    }

}
