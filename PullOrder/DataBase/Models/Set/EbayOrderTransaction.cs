namespace DataBase.Models
{
    public partial class EbayOrderTransaction
    {
        public static EbayOrderTransaction Create(IEbayOrderTransactionMappable src)
        {
            EbayOrderTransaction orderTransaction = new EbayOrderTransaction()
            {
                EbayOrderId = src.GetEbayOrderId(),
                ApiId = src.GetApiId(),
                ApiUserId = src.GetApiUserId(),
                ApiUserPlatformTokenId = src.GetApiUserPlatformTokenId(),
                ItemId = src.GetItemId(),
                ListingType = src.GetListingType()
            };
            Update(orderTransaction, src);
            return orderTransaction;
        }

        public static void Update(EbayOrderTransaction orderTransaction, IEbayOrderTransactionMappable src)
        {
            orderTransaction.BuyerUserId = src.GetBuyerUserId();
            orderTransaction.BuyerEmail = src.GetBuyerEmail();
            orderTransaction.QuantityPurchase = src.GetQuantityPurchase();
            orderTransaction.TransactionId = src.GetTransactionId();
            orderTransaction.TransactionPrice = src.GetTransactionPrice();
            orderTransaction.ItemSku = src.GetItemSku();
            orderTransaction.VariationSku = src.GetVariationSku();
            orderTransaction.Platform = src.GetPlatform();
            orderTransaction.TransactionSiteId = src.GetTransactionSiteId();
            orderTransaction.ShippedTime = src.GetShippedTime();
            orderTransaction.ShipmentCarrierUsed = src.GetShippingCarrierUsed();
            orderTransaction.ShipmentTrackingNumber = src.GetShipmentTrackingNumber();
            orderTransaction.IsShipped = src.GetIsShipped();
        }

        public static string BuildSystemTransactionNo(long seq)
        {
            return $"{"MOT"}{seq.ToString().PadLeft(10, '0')}";
        }

    }

}
