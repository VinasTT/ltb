using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.SMS.Models
{
    public class SMSSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value to sms api password
        /// </summary>
        public string ProviderName { get; set; }
        public string ProviderNameTr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sms service is active
        /// </summary>
        public bool Active { get; set; }
        public bool ActiveTr { get; set; }

        //NOP 3.821
        public string BaseURL { get; set; }
        public string BaseURLTr { get; set; }

        public string Resource { get; set; }
        public string ResourceTr { get; set; }

        public string CountryCode { get; set; }
        public string CountryCodeTr { get; set; }
        //NOP 3.821
        /// <summary>
        /// Gets or sets a value to phonumber sms will be sent
        /// </summary>
        public string PhoneNumber { get; set; }
        public string PhoneNumberTr { get; set; }

        /// <summary>
        /// Gets or sets a value to sms api username
        /// </summary>
        public string UserName { get; set; }
        public string UserNameTr { get; set; }

        /// <summary>
        /// Gets or sets a value to sms api password
        /// </summary>
        public string Password { get; set; }
        public string PasswordTr { get; set; }

        /// <summary>
        /// Gets or sets a value to account sid
        /// </summary>
        public string AccountSid { get; set; }
        public string AccountSidTr { get; set; }

        /// <summary>
        /// Gets or sets a value to authentication token
        /// </summary>
        public string AuthToken { get; set; }
        public string AuthTokenTr { get; set; }

        public string MessageTemplate { get; set; }
        public string MessageTemplateTr { get; set; }

    }
}
