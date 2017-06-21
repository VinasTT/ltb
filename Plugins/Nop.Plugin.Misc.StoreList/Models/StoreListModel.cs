using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Checkout;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.StoreList.Models
{
    public partial class StoreListModel : BaseNopModel
    {
        public StoreListModel()
        {
            AvailableCountries = new List<SelectListItem>();
            AvailableStates = new List<SelectListItem>();
            AvailableDistricts = new List<SelectListItem>(); 
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string District { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public IList<SelectListItem> AvailableCountries { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }
        public IList<SelectListItem> AvailableDistricts { get; set; }

    }
}