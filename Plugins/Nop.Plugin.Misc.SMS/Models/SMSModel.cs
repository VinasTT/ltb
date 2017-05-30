using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Customer;
using FluentValidation.Attributes;
using Nop.Plugin.Misc.SMS.Validators;

namespace Nop.Plugin.Misc.SMS.Models
{
    public class SMSModel
    {
        [NopResourceDisplayName("Plugins.Misc.SMS.ProviderName")]
        public string ProviderName { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.Active")]
        public bool Active { get; set; }

        //NOP 3.821
        [NopResourceDisplayName("Plugins.Misc.SMS.BaseURL")]
        public string BaseURL { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SMS.Resource")]
        public string Resource { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SMS.CountryCode")]
        public string CountryCode { get; set; }

        //NOP 3.821
        [NopResourceDisplayName("Plugins.Misc.SMS.PhoneNumber")]
        public string PhoneNumber { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.UserName")]
        public string UserName { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.Password")]
        public string Password { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.AccountSid")]
        public string AccountSid { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.AuthToken")]
        public string AuthToken { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SMS.MessageTemplate")]
        public string MessageTemplate { get; set; }

        public string Referrer { get; set; }

        public SMSRecord SmsRecordModel { get; set; }
        public RegisterModel RegisterModel { get; set; }
        

    }
}