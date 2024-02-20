using Core.Domain.Database;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IJwtUtils
    {
        string GenerateToken(TblAccount user,int type);
        string GenerateTokenVer(TblAccount tblAccount, object obj, int type);
        int? ValidateToken(string token);
    }
}
