using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class Application
    {
        public int Id { get; set; }
        public int? AccId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
