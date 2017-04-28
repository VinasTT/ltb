using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NOP 3.829
namespace Nop.Core.Domain.Messages
{
    public class SMSNotificationReportResponse
    {
        public List<SentSMSReport> results { get; set; }
        public class SentSMSReport
        {
            public string bulkId { get; set; }
            public string messageId { get; set; }
            public string to { get; set; }
            public string from { get; set; }
            public DateTime sentAt { get; set; }
            public DateTime doneAt { get; set; }
            public int smsCount { get; set; }
            public string mccMnc { get; set; }
            public string callbackData { get; set; }

            public Price price { get; set; }
            public Status status { get; set; }
            public Error error { get; set; }
            
            public class Price
            {
                public decimal pricePerMessage { get; set; }
                public string currency { get; set; }
            }

            public class Status
            {
                public int groupId { get; set; }
                public string groupName { get; set; }
                public int id { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public string action { get; set; }
            }

            public class Error
            {
                public int groupId { get; set; }
                public string groupName { get; set; }
                public int id { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public bool permanent { get; set; }
            }
        }
    }
}
