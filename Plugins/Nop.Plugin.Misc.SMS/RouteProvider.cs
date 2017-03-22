using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Misc.SMS
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Misc.SMS.RegisterSMS",
                 "RegisterSMS",
                 new { controller = "registersms/", action = "SMSController" },
                 new[] { "Nop.Plugin.Misc.SMS.Controllers" }
            );

            /*
            routes.MapRoute("Plugin.Misc.SMS.RegisterResultSMS",
                 "RegisterResultSMS",
                 new { controller = "registerresultsms/", action = "SMSController" },
                 new[] { "Nop.Plugin.Misc.SMS.Controllers" }
            );*/
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
