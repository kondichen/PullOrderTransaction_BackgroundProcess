using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("ebayorder_transaction")]
    public partial class EbayOrderTransaction
    {
        public Guid EbayOrderTransactionId { get; set; }
        public Guid EbayOrderId { get; set; }
        public string ApiId { get; set; }
        public string ApiUserId { get; set; }
        public long ApiUserPlatformTokenId { get; set; }
        public string MarketPlaceTransactionNumber { get; set; }
        public string BuyerUserId { get; set; }
        public string BuyerEmail { get; set; }
        public string ItemId { get; set; }
        public string ListingType { get; set; }
        public int QuantityPurchase { get; set; }
        public string TransactionId { get; set; }
        public decimal TransactionPrice { get; set; }
        public string ItemSku { get; set; }
        public string VariationSku { get; set; }
        public string Platform { get; set; }
        public string ShipmentTrackingNumber { get; set; }
        public string ShipmentCarrierUsed { get; set; }
        public DateTime? ShippedTime { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsHandleByApp { get; set; }
        public string ItemSite { get; set; }
        public string TransactionSiteId { get; set; }
        public bool IsShipped { get; set; }
    }
}
