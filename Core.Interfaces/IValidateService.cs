using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IValidateService
    {
        void ValidatetionException(int? ErrorId, IEnumerable<string> ParamFormat);
        bool HasSomeError();
        void ClearError();
    }
}
