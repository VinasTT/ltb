using System;

//NOP 3.829
namespace Nop.Core.Domain.Messages
{
    
    public class SMSNotificationLog : BaseEntity
    {
        
        public virtual string PhoneNumber { get; set; }
      
        public virtual string Message { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime CreatedOnUtc { get; set; }
    }
}
