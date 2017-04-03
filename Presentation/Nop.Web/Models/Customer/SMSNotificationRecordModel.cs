using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Validators.Customer;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

//BUGFIX 3.803
namespace Nop.Web.Models.Customer
{
    [Validator(typeof(SMSNotificationValidator))]
    public class SMSNotificationRecordModel : BaseNopModel
    {
      
        /// <summary>
        /// Gets or sets the phone number
        /// </summary>

        [NopResourceDisplayName("Account.ValidatePhone.PhoneNumber")]
        [AllowHtml]
        public virtual string PhoneNumber { get; set; }

    }
}