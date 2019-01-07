using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Models
{
    public interface IEbayOrderMappable
    {
        string GetApiId();//
        string GetApiUserId();//
        long GetApiUserPlatformTokenId();
        string GetOrderId();
        string GetOrderStatus();
        decimal GetOrderSubtotal();
        decimal GetOrderTotal();
        DateTime GetOrderCreateAt();
        string GetShippingName();
        string GetShippingStreet();
        string GetShippingStreet1();
        string GetShippingStreet2();
        string GetShippingCityName();
        string GetShippingState();
        string GetShippingCountry();
        string GetShippingCountryName();
        string GetShippingPhone();
        string GetShippingPostalCode();
        string GetShippingAddressId();
        string GetShippingService();
        int GetShippingTimeMin();
        int GetShippingTimeMax();
        string GetBuyerEbayUserId();
        DateTime? GetPaymentTime();
    }

}
