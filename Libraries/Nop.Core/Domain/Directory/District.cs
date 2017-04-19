using Nop.Core.Domain.Localization;
//NOP 3.828
namespace Nop.Core.Domain.Directory
{
    public class District : BaseEntity, ILocalizedEntity
    {
        public int StateProvinceId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public bool? LongDistance { get; set; }
    }
}
