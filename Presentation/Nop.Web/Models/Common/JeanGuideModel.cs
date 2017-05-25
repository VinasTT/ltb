using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.Common;
//NOP 3.832
namespace Nop.Web.Models.Common
{
    public partial class JeanGuideModel : BaseNopModel
    {
        public int Id { get; set; }

        [NopResourceDisplayName("JeanGuide.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("JeanGuide.Rise")]
        public string Rise { get; set; }

        [NopResourceDisplayName("JeanGuide.Cut")]
        public string Cut { get; set; }

        [NopResourceDisplayName("JeanGuide.InseamOne")]
        public string InseamOne { get; set; }

        [NopResourceDisplayName("JeanGuide.InseamTwo")]
        public string InseamTwo { get; set; }

        [NopResourceDisplayName("JeanGuide.InseamThree")]
        public string InseamThree { get; set; }

        [NopResourceDisplayName("JeanGuide.LegOpening")]
        public string LegOpening { get; set; }

        [NopResourceDisplayName("JeanGuide.AdditionalInfo")]
        public string AdditionalInfo { get; set; }

        [NopResourceDisplayName("JeanGuide.Pocket")]
        public string Pocket { get; set; }

        [NopResourceDisplayName("JeanGuide.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("JeanGuide.ImageUrl")]
        public string ImageUrl { get; set; }
        
        public JeanGuideCategoryModel JeanGuideCategory { get; set; }
    }
}