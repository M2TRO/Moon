using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Validate
    {
        public int ErrorId { get; set; } 
        public IEnumerable<string> Errors { get; set; }
        
    }
   
}
