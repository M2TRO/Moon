using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Attribute
{
    public class ErrorCustom
    {
        public ErrorCustom() { }
        public ErrorCustom(string message) {
            Message = message;
        }

        public  ErrorCustom(int errorId)
        {
            ErrorId = errorId;
        }
        public int ErrorId { get; set; }
        public string Message { get; set; }
    }
}
