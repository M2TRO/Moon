using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class LogsMsgsm
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Sender { get; set; }
        public string? Msg { get; set; }
        public decimal? Amout { get; set; }
        public string? Detail { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
