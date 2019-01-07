using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class CompleteSalesRequestItem
    {
        public long CompleteSalesRequestItemId { get; set; }
        public string ApiId { get; set; }
        public string ApiUserId { get; set; }
        public string CompleteSalesRequestId { get; set; }
        public string EbayOrderTransactionId { get; set; }
        public string TrackingNumber { get; set; }
        public string Courier { get; set; }
        public DateTime ShipDate { get; set; }
        public sbyte Success { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
