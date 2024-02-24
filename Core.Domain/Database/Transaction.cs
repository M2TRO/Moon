using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int? BankId { get; set; }
        public int? TransTypeId { get; set; }
        public string? AccRef { get; set; }
        public decimal? Amout { get; set; }
        public DateTime? CeatedDate { get; set; }
    }
}
