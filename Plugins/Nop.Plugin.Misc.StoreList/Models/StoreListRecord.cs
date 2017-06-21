using Nop.Core.Domain.Localization;
using Nop.Core;
using Nop.Core.Domain.Directory;
//NOP 3.828
namespace Nop.Plugin.Misc.StoreList.Models
{
    public class StoreListRecord : BaseEntity, ILocalizedEntity
    {
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

    }
}
