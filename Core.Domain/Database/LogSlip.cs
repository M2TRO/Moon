using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class LogSlip
    {
        public int Id { get; set; }
        public string? Ref { get; set; }
        public string? Bank { get; set; }
        public decimal? Amt { get; set; }
        public bool? IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Datetime { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
