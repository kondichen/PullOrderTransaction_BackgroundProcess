using System;

namespace DataBase.Models
{
    public interface IEbayOrderTransactionMappable
    {
        string GetApiId();//
        string GetApiUserId();//
        long GetApiUserPlatformTokenId();
        Guid GetEbayOrderId();//
        string GetBuyerUserId();
        string GetBuyerEmail();
        string GetItemId();
        string GetListingType();
        int GetQuantityPurchase();
        string GetTransactionId();
        decimal GetTransactionPrice();
        string GetItemSku();
        string GetVariationSku();
        string GetPlatform();
        string GetTransactionSiteId();
        DateTime? GetShippedTime();

        string GetShippingCarrierUsed();
        string GetShipmentTrackingNumber();

        bool GetIsShipped();
    }
}
