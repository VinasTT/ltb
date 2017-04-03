using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NOP 3.824
namespace Nop.Core.Domain.Messages
{
    public class SMSNotificationResponse
    {
        public List<SMSResponseMessage> messages { get; set; }
        public class SMSResponseMessage
        {
            public string to { get; set; }
            public SMSResponseStatus status { get; set; }
            public class SMSResponseStatus
            {
                public string id { get; set; }
                public string groupId { get; set; }
                public string groupName { get; set; }
                public string name { get; set; }
                public string description { get; set; }
            }

            public int smsCount { get; set; }
            public string messageId { get; set; }
        }
    }
}
