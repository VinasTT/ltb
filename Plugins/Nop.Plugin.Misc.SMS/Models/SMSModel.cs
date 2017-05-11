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

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.ProviderNameTr")]
        public string ProviderNameTr { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SMS.Active")]
        public bool Active { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.ActiveTr")]
        public bool ActiveTr { get; set; }

        //NOP 3.821
        [NopResourceDisplayName("Plugins.Misc.SMS.BaseURL")]
        public string BaseURL { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.BaseURLTr")]
        public string BaseURLTr { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SMS.Resource")]
        public string Resource { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.ResourceTr")]
        public string ResourceTr { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SMS.CountryCode")]
        public string CountryCode { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.CountryCodeTr")]
        public string CountryCodeTr { get; set; }

        //NOP 3.821
        [NopResourceDisplayName("Plugins.Misc.SMS.PhoneNumber")]
        public string PhoneNumber { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.PhoneNumberTr")]
        public string PhoneNumberTr { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.UserName")]
        public string UserName { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.UserNameTr")]
        public string UserNameTr { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.Password")]
        public string Password { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.PasswordTr")]
        public string PasswordTr { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.AccountSid")]
        public string AccountSid { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.AccountSidTr")]
        public string AccountSidTr { get; set; }


        [NopResourceDisplayName("Plugins.Misc.SMS.AuthToken")]
        public string AuthToken { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.AuthTokenTr")]
        public string AuthTokenTr { get; set; }

        [NopResourceDisplayName("Plugins.Misc.SMS.MessageTemplate")]
        public string MessageTemplate { get; set; }

        //BUGFIX 3.812
        [NopResourceDisplayName("Plugins.Misc.SMS.MessageTemplateTr")]
        public string MessageTemplateTr { get; set; }

        public SMSRecord SmsRecordModel { get; set; }
        public RegisterModel RegisterModel { get; set; }

        //BUGFIX 3.812
        public int LanguageId { get; set; }

    }
}