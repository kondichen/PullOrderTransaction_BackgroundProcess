namespace DataBase.Models
{
    public partial class EbayOrder
    {
        public static EbayOrder Create(IEbayOrderMappable src)
        {
            EbayOrder ebayOrder = new EbayOrder()
            {
                ApiUserId = src.GetApiUserId(),
                ApiId = src.GetApiId(),
                OrderId = src.GetOrderId(),
                ApiUserPlatformTokenId = src.GetApiUserPlatformTokenId()
            };

            Update(ebayOrder, src);
            return ebayOrder;
        }

        public static void Update(EbayOrder ebayOrder, IEbayOrderMappable src)
        {
            ebayOrder.OrderStatus = src.GetOrderStatus();
            ebayOrder.OrderSubtotal = src.GetOrderSubtotal();
            ebayOrder.OrderTotal = src.GetOrderTotal();
            ebayOrder.OrderCreateAt = src.GetOrderCreateAt();
            ebayOrder.ShippingName = src.GetShippingName();
            ebayOrder.ShippingStreet = src.GetShippingStreet();
            ebayOrder.ShippingStreet1 = src.GetShippingStreet1();
            ebayOrder.ShippingStreet2 = src.GetShippingStreet2();
            ebayOrder.ShippingCityName = src.GetShippingCityName();
            ebayOrder.ShippingState = src.GetShippingState();
            ebayOrder.ShippingCountry = src.GetShippingCountry();
            ebayOrder.ShippingCountryName = src.GetShippingCountryName();
            ebayOrder.ShippingPhone = src.GetShippingPhone();
            ebayOrder.ShippingPostalCode = src.GetShippingPostalCode();
            ebayOrder.ShippingAddressId = src.GetShippingAddressId();
            ebayOrder.ShippingService = src.GetShippingService();
            ebayOrder.ShippingTimeMin = src.GetShippingTimeMin();
            ebayOrder.ShippingTimeMax = src.GetShippingTimeMax();
            ebayOrder.BuyerEbayUserId = src.GetBuyerEbayUserId();
            if (ebayOrder.PaymentTime == null)
            {
                ebayOrder.PaymentTime = src.GetPaymentTime();
            }
        }

        public static string BuildOrderNo(string seqEnd)
        {
            return string.Format("{0}{1}", "MO", seqEnd.ToString().PadLeft(10, '0'));
        }

    }
}
