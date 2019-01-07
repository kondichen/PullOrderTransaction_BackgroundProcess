using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class CompleteSalesRequest
    {
        public string CompleteSalesRequestId { get; set; }
        public string ApiId { get; set; }
        public string ApiUserId { get; set; }
        public int NumberOfItems { get; set; }
        public string CallBackUrl { get; set; }
        public DateTime RequestOn { get; set; }
        public string RequestBy { get; set; }
    }
}
