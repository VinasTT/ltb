using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Validators.Directory;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;
//NOP 3.828
namespace Nop.Admin.Models.Directory
{
   
    public partial class DistrictModel : BaseNopEntityModel, ILocalizedModel<DistrictLocalizedModel>
    {

        public DistrictModel()
        {
            Locales = new List<DistrictLocalizedModel>();
        }

        public int StateProvinceId { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Districts.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Districts.Fields.Abbreviation")]
        [AllowHtml]
        public string Abbreviation { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Districts.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Districts.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Countries.Districts.Fields.LongDistance")]
        public bool? LongDistance { get; set; }

        public IList<DistrictLocalizedModel> Locales { get; set; }
    }

    public partial class DistrictLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }
        
        [NopResourceDisplayName("Admin.Configuration.Countries.Districts.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }
    }
}