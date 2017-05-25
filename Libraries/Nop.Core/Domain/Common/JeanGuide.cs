using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Localization;
//NOP 3.832
namespace Nop.Core.Domain.Common
{
    public partial class JeanGuide : BaseEntity, ILocalizedEntity {

        public string Name { get; set; }
        
        public string Rise { get; set; }
        
        public string Cut { get; set; }
        
        public string InseamOne { get; set; }
        public string InseamTwo { get; set; }
        public string InseamThree { get; set; }

        public string LegOpening { get; set; }

        public string AdditionalInfo { get; set; }
        
        public string Pocket { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public int JeanGuideCategoryId { get; set; }

        public JeanGuideCategory JeanGuideCategory { get; set; }
    }
}