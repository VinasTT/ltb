using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using System.ComponentModel.DataAnnotations.Schema;
using Nop.Core;
using FluentValidation.Attributes;
using Nop.Plugin.Misc.SMS.Validators;

namespace Nop.Plugin.Misc.SMS.Models
{
    [Validator(typeof(SMSValidator))]
    public class SMSRecord : BaseEntity
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

        public virtual bool Active { get; set; } //BUGFIX 3.803
    }
}
