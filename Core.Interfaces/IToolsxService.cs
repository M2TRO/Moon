using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IToolsxService
    {
        void ConverBase(SingleFileModel img64);
        ResultUpload Upload(SingleFileModel input);
    }
}
