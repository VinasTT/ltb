using System.Web.Mvc;
using Nop.Services.Common;
using Nop.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Services.Localization;
//NOP 3.832
namespace Nop.Web.Controllers
{
    public partial class JeanGuideController : BasePublicController
    {
        #region Fields

        private readonly IJeanGuideService _jeanGuideService;

        #endregion

        #region Constructors

        public JeanGuideController(IJeanGuideService jeanGuideService)
        {
            this._jeanGuideService = jeanGuideService;
        }

        #endregion

        #region Utilities

        
        #endregion

        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JeanGuide(string genre)
        {
            var jeanGuideCategories = _jeanGuideService.GetJeanGuideCategoriesByGenre(genre);
            if (!jeanGuideCategories.Any())
                return InvokeHttp404();

            var model = new List<JeanGuideCategoryModel>();

            foreach (var item in jeanGuideCategories)
            {
                var jeanGuideCategoryModel = new JeanGuideCategoryModel();
                jeanGuideCategoryModel.Id = item.Id;
                jeanGuideCategoryModel.Name = item.GetLocalized(x => x.Name);
                jeanGuideCategoryModel.ImageUrl = item.ImageUrl;
                jeanGuideCategoryModel.Description = item.GetLocalized(x => x.Description);
                jeanGuideCategoryModel.Genre = item.GetLocalized(x => x.Genre);
                model.Add(jeanGuideCategoryModel);
            }

            return View(model);
        }

        public ActionResult JeanGuideDetail(int jeanGuideId)
        {
            var jeanGuides = _jeanGuideService.GetJeanGuidesByCategoryId(jeanGuideId);

            if (!jeanGuides.Any())
                return InvokeHttp404();

            var model = new List<JeanGuideModel>();

            foreach (var item in jeanGuides)
            {
                var jeanGuideModel = new JeanGuideModel();
                var jeanGuideCategory = _jeanGuideService.GetJeanGuideCategoryById(item.JeanGuideCategoryId);
                if (jeanGuideCategory == null)
                    return InvokeHttp404();
                jeanGuideModel.JeanGuideCategory = new JeanGuideCategoryModel();
                jeanGuideModel.JeanGuideCategory.Name = jeanGuideCategory.Name;
                jeanGuideModel.Id = item.Id;
                jeanGuideModel.Name = item.Name;
                jeanGuideModel.ImageUrl = item.ImageUrl;
                jeanGuideModel.Description = item.Description;
                jeanGuideModel.Cut = item.Cut;
                jeanGuideModel.AdditionalInfo = item.AdditionalInfo;
                jeanGuideModel.InseamOne = item.InseamOne;
                jeanGuideModel.InseamTwo = item.InseamTwo;
                jeanGuideModel.InseamThree = item.InseamThree;
                jeanGuideModel.LegOpening = item.LegOpening;
                jeanGuideModel.Pocket = item.Pocket;
                jeanGuideModel.Rise = item.Rise;
                model.Add(jeanGuideModel);
            }

            return View(model);
        }

        public ActionResult ShopByFit(int jeanGuideId)
        {
            var jeanGuide = _jeanGuideService.GetJeanGuideById(jeanGuideId);
            if (jeanGuide == null)
                return InvokeHttp404();
            var jeanGuideCategory = _jeanGuideService.GetJeanGuideCategoryById(jeanGuide.JeanGuideCategoryId);
            if (jeanGuideCategory == null)
                return InvokeHttp404();
            var categoryId = _jeanGuideService.GetJeanGuideNopCategoryIdByName(jeanGuideCategory.Name);
            

            return RedirectToAction("ShopByFit", "Catalog", new { categoryId = categoryId, keyword = jeanGuide.Name.ToLower() });
        }

        #endregion
    }
}
