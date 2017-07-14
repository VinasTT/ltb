﻿using System;
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
using Nop.Plugin.Widgets.ProductInStore.Models;

namespace Nop.Plugin.Widgets.ProductInStore
{
    public class ProductInStore : BasePlugin, IWidgetPlugin
    {

        private readonly ProductInStoreObjectContext _context;

        public ProductInStore(ProductInStoreObjectContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "productdetails_bottom" };
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
            controllerName = "ProductInStore";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Widgets.ProductInStore.Controllers" }, { "area", null } };
        }


        /// <summary>
        /// Gets a route for displaying widget
        /// </summary>
        /// <param name="widgetZone">Widget zone where it's displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "ProductInStore";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.ProductInStore.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
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
