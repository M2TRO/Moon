using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }

    public class DomainSettings
    {
        public string CoreAPIUrl { get; set; }
    }

    public class URLAPI
    {
        public string WarpPortalAPI { get; set; }
        public string PromptpayAPI { get; set; }
    }
}
