using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class LogEvent
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Addr { get; set; }
        public string? Detail { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
