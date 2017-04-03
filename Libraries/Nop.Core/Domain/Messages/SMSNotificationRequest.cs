using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NOP 3.824
namespace Nop.Core.Domain.Messages
{
    public class SMSNotificationRequest
    {
        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }
    }
}
