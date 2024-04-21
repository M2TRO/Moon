using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int? LogId { get; set; }
        public int? TransBankId { get; set; }
        public int? TransTypeId { get; set; }
        public string? AccRef { get; set; }
        public string? Sender { get; set; }
        public decimal? Amout { get; set; }
        public decimal? Total { get; set; }
        public DateTime? DateTimeSlip { get; set; }
        public bool? Verfify { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? CeatedDate { get; set; }
    }
}
