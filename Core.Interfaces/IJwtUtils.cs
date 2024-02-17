using Core.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IJwtUtils
    {
        string GenerateToken(TblAccount user);
        int? ValidateToken(string token);
    }
}
