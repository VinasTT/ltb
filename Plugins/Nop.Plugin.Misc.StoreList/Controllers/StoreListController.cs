using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Services.Localization;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Models.Common;
using Nop.Plugin.Misc.StoreList.Services;
using Nop.Plugin.Misc.StoreList.Models;

namespace Nop.Plugin.Misc.District.Controllers
{
    [NopHttpsRequirement(SslRequirement.Yes)]
    public partial class StoreListController : BasePluginController
    {

        private readonly IStoreListService _storeListService;
        private readonly ILocalizationService _localizationService;
        #region Constructors

        public StoreListController(IStoreListService storeListService,
            ILocalizationService localizationService) {
            _storeListService = storeListService;
            _localizationService = localizationService;
        } //NOP 3.828

        #endregion

        #region Methods
        public ActionResult Index()
        {
            var storeList = _storeListService.GetStoreList();
            
            var model = new List<StoreListModel>();

            foreach (var item in storeList)
            {
                var storeListModel = new StoreListModel();
                storeListModel.Id = item.Id;
                storeListModel.Name = item.GetLocalized(x => x.Name);
                storeListModel.Published = item.Published;
                storeListModel.Description = item.GetLocalized(x => x.Description);
                storeListModel.Contact = item.Contact;
                storeListModel.Country = item.GetLocalized(x => x.Country);
                storeListModel.District = item.District;
                storeListModel.StateProvince = item.StateProvince;

                model.Add(storeListModel);
            }

            model.FirstOrDefault().AvailableCountries.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectCountry"), Value = "0" });
            foreach (var c in _storeListService.GetCountryList())
            {
                model.FirstOrDefault().AvailableCountries.Add(new SelectListItem
                {
                    Text = c,
                    Value = c,
                    Selected = c == "0"
                });
            }
            model.FirstOrDefault().AvailableStates.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectState"), Value = "0" });
            model.FirstOrDefault().AvailableDistricts.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectDistrict"), Value = "0" });

            return View("~/Plugins/Misc.StoreList/Views/Index.cshtml", model);
        }

        //available even when navigation is not allowed
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetStatesByCountry(string country, bool addSelectStateItem)
        {
            //this action method gets called via an ajax request
            if (String.IsNullOrEmpty(country))
                throw new ArgumentNullException("country");
            
            var states = _storeListService.GetStateProvinceListByCountry(country).ToList();
            var result = (from s in states
                          select new { id = s, name = s })
                          .ToList();


            if (country == "0")
            {
                //country is not selected ("choose country" item)
                if (addSelectStateItem)
                {
                    result.Insert(0, new { id = "0", name = _localizationService.GetResource("Address.SelectState") });
                }
                else
                {
                    result.Insert(0, new { id = "0", name = _localizationService.GetResource("Address.OtherNonUS") });
                }
            }
            else
            {
                //some country is selected
                if (!result.Any())
                {
                    //country does not have states
                    result.Insert(0, new { id = "0", name = _localizationService.GetResource("Address.OtherNonUS") });
                }
                else
                {
                    //country has some states
                    if (addSelectStateItem)
                    {
                        result.Insert(0, new { id = "0", name = _localizationService.GetResource("Address.SelectState") });
                    }
                }
            }



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDistrictsByState(string country , string state, bool addSelectDistrictItem)
        {
            //this action method gets called via an ajax request
            if (String.IsNullOrEmpty(state))
                throw new ArgumentNullException("country");

            var districts = _storeListService.GetDistrictListByStateProvince(country,state).ToList();
            var result = (from s in districts
                          select new { id = s, name = s })
                          .ToList();


            if (state == "0")
            {
                //country is not selected ("choose country" item)
                if (addSelectDistrictItem)
                {
                    result.Insert(0, new { id = "0", name = _localizationService.GetResource("Address.SelectDistrict") });
                }
                else
                {
                    result.Insert(0, new { id = "0", name = _localizationService.GetResource("Address.OtherNonUS") });
                }
            }
            else
            {
                //some country is selected
                if (!result.Any())
                {
                    //country does not have states
                    result.Insert(0, new { id = "0", name = _localizationService.GetResource("Address.OtherNonUS") });
                }
                else
                {
                    //country has some states
                    if (addSelectDistrictItem)
                    {
                        result.Insert(0, new { id = "0", name = _localizationService.GetResource("Address.SelectDistrict") });
                    }
                }
            }



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //available even when navigation is not allowed
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetStoreList()
        {
            //this action method gets called via an ajax request

            var stores = _storeListService.GetStoreList();
            var result = stores.ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}
