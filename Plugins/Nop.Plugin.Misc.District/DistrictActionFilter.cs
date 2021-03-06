﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.District.Controllers;
using Nop.Plugin.Misc.District.Services;
using Nop.Plugin.Misc.District.Models;
using Nop.Services.Catalog;
using Nop.Web.Models.Customer;
using Nop.Web.Models.Checkout;


namespace Nop.Plugin.Misc.District
{
    public class DistrictActionFilter : ActionFilterAttribute, IFilterProvider, IActionFilter
    {
        private readonly IDistrictService _districtService;

        public DistrictActionFilter(IDistrictService districtService
            )
        {
            this._districtService = districtService;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if (
                controllerContext.Controller is Nop.Web.Controllers.CheckoutController && actionDescriptor.ActionName.Equals("BillingAddress", StringComparison.InvariantCultureIgnoreCase) ||
                controllerContext.Controller is Nop.Web.Controllers.CheckoutController && actionDescriptor.ActionName.Equals("ShippingAddress", StringComparison.InvariantCultureIgnoreCase) ||
                controllerContext.Controller is Nop.Web.Controllers.CustomerController && actionDescriptor.ActionName.Equals("Addresses", StringComparison.InvariantCultureIgnoreCase) ||
                controllerContext.Controller is Nop.Web.Controllers.CustomerController && actionDescriptor.ActionName.Equals("AddressAdd", StringComparison.InvariantCultureIgnoreCase) ||
                controllerContext.Controller is Nop.Web.Controllers.CustomerController && actionDescriptor.ActionName.Equals("AddressEdit", StringComparison.InvariantCultureIgnoreCase) ||
                controllerContext.Controller is Nop.Web.Controllers.ShoppingCartController && actionDescriptor.ActionName.Equals("OrderSummary", StringComparison.InvariantCultureIgnoreCase)
               
                )

            {
                return new List<Filter>() { new Filter(this, FilterScope.Action, 0) };
            }

            return new List<Filter>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var districtController = EngineContext.Current.Resolve<DistrictController>(); // my plugin controller


            if (filterContext.Controller is Nop.Web.Controllers.CheckoutController && filterContext.ActionDescriptor.ActionName.Equals("BillingAddress", StringComparison.InvariantCultureIgnoreCase))
            {
                var form = (FormCollection)filterContext.ActionParameters["form"];
                filterContext.Result = districtController.CheckoutAddress(form); // my controller method

            }
            
            else if (filterContext.Controller is Nop.Web.Controllers.CheckoutController && filterContext.ActionDescriptor.ActionName.Equals("ShippingAddress", StringComparison.InvariantCultureIgnoreCase))
            {
                var form = (FormCollection)filterContext.ActionParameters["form"];
                filterContext.Result = districtController.CheckoutAddress(form); // my controller method

            }

            else if (filterContext.Controller is Nop.Web.Controllers.CustomerController && filterContext.ActionDescriptor.ActionName.Equals("Addresses", StringComparison.InvariantCultureIgnoreCase))
            {
                filterContext.Result = districtController.Addresses(); // my controller method
            }
            else if (filterContext.Controller is Nop.Web.Controllers.CustomerController && filterContext.ActionDescriptor.ActionName.Equals("AddressAdd", StringComparison.InvariantCultureIgnoreCase))
            {
                if (filterContext.HttpContext.Request.HttpMethod == "GET")
                {
                    filterContext.Result = districtController.AddressAdd(); // my controller method
                }
                else
                {
                    var form = (FormCollection)filterContext.ActionParameters["form"];
                    var model = (CustomerAddressEditModel)filterContext.ActionParameters["model"];
                    filterContext.Result = districtController.AddressAdd(model,form); // my controller method
                }
                
            }
            else if (filterContext.Controller is Nop.Web.Controllers.CustomerController && filterContext.ActionDescriptor.ActionName.Equals("AddressEdit", StringComparison.InvariantCultureIgnoreCase))
            {
                var addressId = (int)filterContext.ActionParameters["addressId"];
                if (filterContext.HttpContext.Request.HttpMethod == "GET")
                {
                    filterContext.Result = districtController.AddressEdit(addressId); // my controller method
                }
                else
                {
                    var form = (FormCollection)filterContext.ActionParameters["form"];
                    var model = (CustomerAddressEditModel)filterContext.ActionParameters["model"];
                    filterContext.Result = districtController.AddressEdit(model, addressId, form); // my controller method
                }
            }

            else if (filterContext.Controller is Nop.Web.Controllers.ShoppingCartController && filterContext.ActionDescriptor.ActionName.Equals("OrderSummary", StringComparison.InvariantCultureIgnoreCase))
            {
                bool? prepareAndDisplayOrderReviewData = (bool?)filterContext.ActionParameters["prepareAndDisplayOrderReviewData"];
                filterContext.Result = districtController.OrderSummary(prepareAndDisplayOrderReviewData); // my controller method
            }


        }
    }
}
