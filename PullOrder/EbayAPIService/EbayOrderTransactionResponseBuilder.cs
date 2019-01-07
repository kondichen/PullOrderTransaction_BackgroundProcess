using DataBase.Models;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase
{
    public class EbayOrderTransactionResponseBuilder
    {
        public TransactionType transactionType { get; private set; }
        private EbayOrderTransactionResponse response;

        public EbayOrderTransactionResponseBuilder(TransactionType transactionType)
        {
            this.transactionType = transactionType;
        }

        public EbayOrderTransactionResponse ToResponse()
        {
            response = new EbayOrderTransactionResponse()
            {
                buyerEbayId = transactionType.Buyer.UserID,
                itemId = transactionType.Item.ItemID,
                listingType = transactionType.Item.ListingType.ToString(),
                quantityPurchase = transactionType.QuantityPurchased,
                transactionId = transactionType.TransactionID,
                transactionPrice = transactionType.TransactionPrice.Value,
                platform = transactionType.Platform.ToString(),
                transactionSiteId = transactionType.TransactionSiteID.ToString()
            };

            GetBuyerEamil();
            GetItemSku();
            GetVariationSku();
            GetShippedTime();
            GetShippingCarrierUsed();

            return response;
        }

        private void GetBuyerEamil()
        {
            string email = string.Empty;
            if (!string.IsNullOrEmpty(transactionType.Buyer?.Email))
            {
                email = transactionType.Buyer.Email;
            }

            response.buyerEmail = email;
        }

        private void GetItemSku()
        {
            string sku = null;
            if (!string.IsNullOrEmpty(transactionType.Item?.SKU))
            {
                sku = transactionType.Item.SKU;
            }
            response.itemSku = sku;
        }

        private void GetVariationSku()
        {
            string sku = null;
            if (!string.IsNullOrEmpty(transactionType.Variation?.SKU))
            {
                sku = transactionType.Variation.SKU;
            }
            response.variationSku = sku;
        }

        private void GetShippingCarrierUsed()
        {
            ShipmentTrackingDetailsType[] shipmentTrackingDetailsTypeCollection =
                transactionType.ShippingDetails?.ShipmentTrackingDetails;

            ShipmentTrackingDetailsType shipmentTrackingDetailsType = shipmentTrackingDetailsTypeCollection != null ? (shipmentTrackingDetailsTypeCollection.Count() > 0 ? shipmentTrackingDetailsTypeCollection[0] : null) : null;
            if (shipmentTrackingDetailsType == null)
            {
                return;
            }

            response.shippingCarrierUsed = shipmentTrackingDetailsType.ShippingCarrierUsed;
            response.shipmentTrackingNumber = shipmentTrackingDetailsType.ShipmentTrackingNumber;
        }

        private void GetShippedTime()
        {
            if (transactionType.ShippedTimeSpecified)
            {
                response.shippedTime = transactionType.ShippedTime;
            }
        }
    }
}
