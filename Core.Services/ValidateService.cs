using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Services
{
    public class ValidationService: Exception
    {
        public  IEnumerable<Validate> _validateList { get; set; }


        public ValidationService() { 
        
        }
        public ValidationService(int? ErrorId,IEnumerable<string> ParamFormat)
        {
            var list = new List<Validate>();
            list.Add(new Validate() { ErrorId = ErrorId.Value, Errors =  ParamFormat });
            _validateList = list;
        }

        public ValidationService(string str)
        {
            var list = new List<Validate>();
            list.Add(new Validate() { Errors = new string[] { str } });
            _validateList = list;
        }
        public ValidationService(IEnumerable<string> ParamFormat)
        {
            var list = new List<Validate>();
            list.Add(new Validate() { Errors = ParamFormat });
            _validateList = list;
        }
        public void AddError(string str)
        {
            if(_validateList == null)
            {
                _validateList = new List<Validate>();
            }

            var list = _validateList.ToList();
            list.Add(new Validate {  Errors = new string[] { str } });
            _validateList= list;

        }
        public bool HasSomeError()
        {
            if(_validateList == null)
            {
                return false;
            }
            return _validateList.ToList().Count > 0;
            
        }

        public void ClearError()
        {
            var errorList = _validateList.ToList();
            errorList.Clear();
            this._validateList = errorList;
        }
    }
}
