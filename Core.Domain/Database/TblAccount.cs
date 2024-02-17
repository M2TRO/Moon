using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class TblAccount
    {
        public int Id { get; set; }
        public string AccName { get; set; } = null!;
        public string AccPwd { get; set; } = null!;
        public string AccEmail { get; set; } = null!;
        public string? AccTel { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
