using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class TransVer
    {
        public int Id { get; set; }
        public int? LogId { get; set; }
        public string? OrderId { get; set; }
        public int? TransId { get; set; }
        public string? BankCode { get; set; }
        public string? Amout { get; set; }
        public string? AccountRef { get; set; }
        public bool? State { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
