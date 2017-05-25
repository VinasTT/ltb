using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Localization;
//NOP 3.832
namespace Nop.Core.Domain.Common
{
    public partial class JeanGuideCategory : BaseEntity, ILocalizedEntity 
    {
      
        public string Name { get; set; }       

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Genre { get; set; }

        public IList<JeanGuide> JeanGuides { get; set; }
    }
}