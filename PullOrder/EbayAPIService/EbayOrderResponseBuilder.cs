using DataBase.Models;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase
{

    public class EbayOrderResponseBuilder
    {
        private static readonly DateTime Cutoff = new DateTime(2000, 1, 1);
        public OrderType orderType { get; private set; }
        private EbayOrderResult ebayOrderResult;

        public EbayOrderResponseBuilder(OrderType orderType)
        {
            this.orderType = orderType;
        }

        public EbayOrderResult ToResponse()
        {
            ebayOrderResult = new EbayOrderResult()
            {
                orderId = orderType.OrderID,
                orderSubTotal = orderType.Subtotal.Value,
                orderTotal = orderType.Total.Value,
                paymentTime = GetpaymentInfo(),
                orderCreateAt = orderType.CreatedTime
            };

            GetShippingInfo();
            GetShippingTimeInfo();
            ebayOrderResult.orderStatus = GetOrderStatus();
            ebayOrderResult.buyerUserid = orderType.BuyerUserID;
            GetShippedTime();
            GetShipmentInfo();
            GetTransaction();
            return ebayOrderResult;
        }

        private void GetShippingInfo()
        {
            AddressType addressType = orderType.ShippingAddress;
            ebayOrderResult.shippingName = addressType.Name;
            ebayOrderResult.shippingStreet = addressType.Street;
            ebayOrderResult.shippingStreet1 = addressType.Street1;
            ebayOrderResult.shippingStreet2 = addressType.Street2;
            ebayOrderResult.shippingCityName = addressType.CityName;
            ebayOrderResult.shippingState = addressType.StateOrProvince;
            ebayOrderResult.shippingCountry = addressType.Country.ToString();
            ebayOrderResult.shippingCountryName = addressType.CountryName;
            ebayOrderResult.shippingPhone = addressType.Phone;
            ebayOrderResult.shippingPostalCode = addressType.PostalCode;
            ebayOrderResult.shippingAddressId = addressType.AddressID;

        }

        private void GetShippedTime()
        {
            if (orderType.ShippedTimeSpecified)
            {
                ebayOrderResult.shippedTime = orderType.ShippedTime;
            }
        }

        private void GetTransaction()
        {
            ebayOrderResult.orderTransactions = new List<EbayOrderTransactionResponse>();
            foreach (TransactionType transactionType in orderType.TransactionArray)
            {
                EbayOrderTransactionResponseBuilder builder = new EbayOrderTransactionResponseBuilder(transactionType);
                EbayOrderTransactionResponse ebayOrderTransactionResponse = builder.ToResponse();
                FillOrderShipmentInfo(ebayOrderTransactionResponse);
                ebayOrderResult.orderTransactions.Add(ebayOrderTransactionResponse);
            }
            ebayOrderResult.OrderTransIdList = new List<string>();
            ebayOrderResult.OrderTransIdList = ebayOrderResult.orderTransactions.Select(x => x.transactionId).ToList();
        }

        private void FillOrderShipmentInfo(EbayOrderTransactionResponse transactionResponse)
        {
            if (string.IsNullOrEmpty(transactionResponse.shipmentTrackingNumber))
            {
                transactionResponse.shipmentTrackingNumber = ebayOrderResult.shipmentTrackingNumber;
            }

            if (string.IsNullOrEmpty(transactionResponse.shippingCarrierUsed))
            {
                transactionResponse.shippingCarrierUsed = ebayOrderResult.shippingCarrierUsed;
            }

            if (!transactionResponse.shippedTime.HasValue)
            {
                transactionResponse.shippedTime = ebayOrderResult.shippedTime;
            }
        }

        private void GetShippingTimeInfo()
        {
            ShippingServiceOptionsType serviceOptionsType = orderType.ShippingServiceSelected;
            ebayOrderResult.shippingService = serviceOptionsType.ShippingService;
            ebayOrderResult.shippingTimeMin = serviceOptionsType.ShippingTimeMin;
            ebayOrderResult.shippingTimeMax = serviceOptionsType.ShippingTimeMax;
        }

        private DateTime? GetpaymentInfo()
        {
            DateTime? paymenTime = null;
            PaymentsInformationType paymentInfo = orderType.MonetaryDetails;
            if (paymentInfo?.Payments?.Payment != null && paymentInfo.Payments.Payment.Count() > 0)
            {
                paymenTime = paymentInfo.Payments.Payment[0].PaymentTime;
            }

            if (paymenTime?.CompareTo(Cutoff) < 0)
            {
                paymenTime = null;
            }

            return paymenTime;
        }

        private string GetOrderStatus()
        {
            string r;
            if (orderType.OrderStatus == OrderStatusCodeType.Completed)
            {
                r = ebayOrderResult.paymentTime.HasValue ? orderType.OrderStatus.ToString() : "Unpaid";
            }
            else
            {
                r = orderType.OrderStatus.ToString();
            }
            return r;
        }

        private void GetShipmentInfo()
        {
            ShipmentTrackingDetailsType[] detailsTypeCollection =
                orderType.ShippingDetails.ShipmentTrackingDetails;

            ShipmentTrackingDetailsType detailsType = detailsTypeCollection != null ? (detailsTypeCollection.Count() > 0 ? detailsTypeCollection[0] : null) : null;
            if (detailsType == null)
            {
                return;
            }

            ebayOrderResult.shippingCarrierUsed = detailsType.ShippingCarrierUsed;
            ebayOrderResult.shipmentTrackingNumber = detailsType.ShipmentTrackingNumber;
        }
    }


}
