using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class MtBank
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? DescEn { get; set; }
        public string? DescTh { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
