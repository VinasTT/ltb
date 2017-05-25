using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.Common;
//NOP 3.832
namespace Nop.Web.Models.Common
{
    public partial class JeanGuideCategoryModel : BaseNopModel
    {
        public int Id { get; set; }

        [NopResourceDisplayName("JeanGuide.Category.Name")]
        public string Name { get; set; }       

        [NopResourceDisplayName("JeanGuide.Category.Description")]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public string Genre { get; set; }
        
    }
}