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

        /// <summary>
        /// Gets or sets a value indicating whether the sms service is active
        /// </summary>
        public bool Active { get; set; }

        //NOP 3.821
        public string BaseURL { get; set; }

        public string Resource { get; set; }

        public string CountryCode { get; set; }
        //NOP 3.821
        /// <summary>
        /// Gets or sets a value to phonumber sms will be sent
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value to sms api username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value to sms api password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value to account sid
        /// </summary>
        public string AccountSid { get; set; }

        /// <summary>
        /// Gets or sets a value to authentication token
        /// </summary>
        public string AuthToken { get; set; }

        public string MessageTemplate { get; set; }

    }
}
