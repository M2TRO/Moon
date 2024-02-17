using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class TblScheduler
    {
        public int Id { get; set; }
        public int? AccId { get; set; }
        public int? AppId { get; set; }
        public string? TitelName { get; set; }
        public string? Hashtag { get; set; }
        public string? RefNo { get; set; }
        public DateTime? ActionTime { get; set; }
        public bool? State { get; set; }
        public string? ModifyBy { get; set; }
        public string? ModifyTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
