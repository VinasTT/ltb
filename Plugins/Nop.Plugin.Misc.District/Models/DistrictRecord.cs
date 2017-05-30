using Nop.Core.Domain.Localization;
using Nop.Core;
//NOP 3.828
namespace Nop.Plugin.Misc.District.Models
{
    public class DistrictRecord : BaseEntity, ILocalizedEntity
    {
        public int StateProvinceId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public bool? LongDistance { get; set; }
    }
}
