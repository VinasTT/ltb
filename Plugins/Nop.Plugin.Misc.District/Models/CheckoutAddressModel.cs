using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Checkout;

namespace Nop.Plugin.Misc.District.Models
{
    public partial class CheckoutAddressModel : BaseNopModel
    {
        public CheckoutAddressModel()
        {
            CheckoutShippingAddressModel = new CheckoutShippingAddressModel();
            CheckoutBillingAddressModel = new CheckoutBillingAddressModel();
        }

        public CheckoutShippingAddressModel CheckoutShippingAddressModel { get; set; }

        public CheckoutBillingAddressModel CheckoutBillingAddressModel { get; set; }

    }
}