//NOP 3.824
namespace Nop.Core.Domain.Messages
{
    
    public class SMSNotificationRecord : BaseEntity
    {
        /// <summary>
        /// Gets or sets the customer id
        /// </summary>
        public virtual int CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public virtual string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets the activation code
        /// </summary>
        public virtual string ActivationCode { get; set; }

        public virtual bool Active { get; set; }
    }
}
