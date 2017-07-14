using System.Data.Entity.ModelConfiguration;


namespace Nop.Plugin.Widgets.ProductInStore.Models
{
    public class ProductInStoreMap : EntityTypeConfiguration<ProductInStoreRecord>
    {
        public ProductInStoreMap()
        {
            ToTable("Product_StoreList_Mapping");

            HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.ProductId);
            Property(m => m.StoreListId);
            Property(m => m.Quantity);
           
        }
    }
}
