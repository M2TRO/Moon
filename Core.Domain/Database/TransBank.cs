using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class TransBank
    {
        public int Id { get; set; }
        public int? AccId { get; set; }
        public string? PromNo { get; set; }
        public string? Token { get; set; }
        public int? TypeId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
