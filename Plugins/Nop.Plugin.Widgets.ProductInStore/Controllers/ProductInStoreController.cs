using System.Web.Mvc;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.ProductInStore.Controllers
{
    public class ProductInStoreController : BasePluginController
    {
       

        public ProductInStoreController()
        {
            
        }
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
         
            return View("~/Plugins/Widgets.ProductInStore/Views/Configure.cshtml");
        }


        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            
            return View("~/Plugins/Widgets.ProductInStore/Views/PublicInfo.cshtml");
        }

        
        
    }
}
