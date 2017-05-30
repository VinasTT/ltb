using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Misc.District
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {

            //NOP 3.828
            //get district list by state ID  (AJAX link)
            routes.MapRoute("GetDistrictsByStateId",
                            "district/getdistrictsbystateid/",
                            new { controller = "District", action = "GetDistrictsByStateId" },
                            new[] { "Nop.Plugin.Misc.District.Controllers" });

            /*routes.MapRoute("CustomerAddressEdit",
                            "customer/addressedit/{addressId}",
                            new { controller = "District", action = "AddressEdit" },
                            new { addressId = @"\d+" },
                            new[] { "Nop.Plugin.Misc.District.Controllers" });*/
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
