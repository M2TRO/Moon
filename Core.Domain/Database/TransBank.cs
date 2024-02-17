using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class TransBank
    {
        public int Id { get; set; }
        public string? PId { get; set; }
        public int? AccId { get; set; }
        public int? TypeId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
