using Nop.Core.Domain.Localization;
using Nop.Core;
using Nop.Plugin.Misc.StoreList.Models;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Widgets.ProductInStore.Models
{
    public class ProductInStoreRecord : BaseEntity, ILocalizedEntity
    {
        public int ProductId { get; set; }
        public int StoreListId { get; set; }
        public int Quantity { get; set; }
    }
}
