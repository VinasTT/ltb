using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Misc.StoreList
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {

            //NOP 3.828
            //get district list by state ID  (AJAX link)
            routes.MapRoute("StoreList",
                            "storelist/index/",
                            new { controller = "StoreList", action = "Index" },
                            new[] { "Nop.Plugin.Misc.StoreList.Controllers" });

            

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
