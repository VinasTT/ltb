﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Plugins;
using Nop.Services.Common;
using System.Web.Routing;
using Nop.Plugin.Misc.SMS.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace Nop.Plugin.Misc.SMS
{
    public class SMS : BasePlugin, IMiscPlugin
    {

        private readonly SMSObjectContext _objectContext;
        private readonly ISettingService _settingService;

        public SMS(SMSObjectContext objectContext,
            ISettingService settingService)
        {
            this._objectContext = objectContext;
            this._settingService = settingService;
        }
            

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "SMS";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Misc.SMS.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            var settings = new SMSSettings
            {
                ProviderName = "Twilio",
                Active = true,
                PhoneNumber = "05327801742",
                UserName = "ltbadmin",
                Password = "test",
                AccountSid = "ACcace8ef80331044824b975feabebc5a7",
                AuthToken = "your_auth_token",
                MessageTemplate = "Your activation code is"
            };
            _settingService.SaveSetting(settings);

            _objectContext.Install();

            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.ProviderName", "Provide Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.Active", "Active");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.PhoneNumber", "Phone Number to send sms from");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.UserName", "UserName");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.Password", "Password");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.AccountSid", "AccountSid");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.AuthToken", "AuthToken");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.MessageTemplate", "MessageTemplate");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.SMS.PhoneNumberFormat", "Please specify phone number in format 0XXXXXXXXXX");

            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<SMSSettings>();

            _objectContext.Uninstall();
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);

            //locales
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.ProviderName");
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.Active");
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.UserName");
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.Password");
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.PhoneNumber");
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.AccountSid");
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.AuthToken");
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.MessageTemplate");
            this.DeletePluginLocaleResource("Plugins.Misc.SMS.PhoneNumberFormat");

            base.Uninstall();
        }
    }
}