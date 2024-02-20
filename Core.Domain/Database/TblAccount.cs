using System;
using System.Collections.Generic;

namespace Core.Domain.Database
{
    public partial class TblAccount
    {
        public int Id { get; set; }
        public string? AccRef { get; set; }
        public string? AccName { get; set; }
        public string? AccPwd { get; set; }
        public string? AccEmail { get; set; }
        public string AccTel { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
