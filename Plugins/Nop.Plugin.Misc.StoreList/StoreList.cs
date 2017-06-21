using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Routing;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Common;
using Nop.Plugin.Misc.StoreList.Models;
namespace Nop.Plugin.Misc.StoreList
{
    public class StoreList : BasePlugin, IMiscPlugin
    {

        private readonly StoreListObjectContext _context;

        public StoreList(StoreListObjectContext context)
        {
            _context = context;
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
            controllerName = "StoreList";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Misc.StoreList.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            _context.Install();
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            _context.Uninstall();
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);
            base.Uninstall();
        }
    }
}
