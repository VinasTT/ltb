using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.SMS.Controllers;
using Nop.Plugin.Misc.SMS.Models;
using Nop.Plugin.Misc.SMS.Services;
using Nop.Services.Catalog;


namespace Nop.Plugin.Misc.SMS
{
    public class SMSActionFilter : ActionFilterAttribute, IFilterProvider, IActionFilter
    {
        private readonly ISMSService _smsService;
        private readonly IProductService _productService;

        public SMSActionFilter(ISMSService mvpService,
            IProductService productService
            )
        {
            this._productService = productService;
            this._smsService = mvpService;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if (controllerContext.Controller is Nop.Web.Controllers.CustomerController && actionDescriptor.ActionName.Equals("Register", StringComparison.InvariantCultureIgnoreCase))
            {
                return new List<Filter>() { new Filter(this, FilterScope.Action, 0) };
            }

            return new List<Filter>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;
            //var routeValues = new RouteValueDictionary();

            //var product = _productService.GetProductById(productId);

            if (httpMethod == "GET")
            {
                var smsController = EngineContext.Current.Resolve<SMSController>(); // my plugin controller
                filterContext.Result = smsController.Register(); // my controller method
            }
            else
            {
                var smsModel = (SMSModel)filterContext.RouteData.Values["smsmodel"];
                var stringurl = (string)filterContext.RouteData.Values["stringurl"];
                var captchaValid = Convert.ToBoolean(filterContext.RouteData.Values["captchavalid"]);
                var form = (FormCollection)filterContext.RouteData.Values["form"];
                var smsController = EngineContext.Current.Resolve<SMSController>(); // my plugin controller
                filterContext.Result = smsController.RegisterPOST(smsModel, stringurl, captchaValid, form); // my controller method
            }
        }
    }
}
