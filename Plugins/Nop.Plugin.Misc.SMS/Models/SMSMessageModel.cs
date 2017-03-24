using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.SMS.Models
{
    public class SMSMessageModel
    {
        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }
    }
}
