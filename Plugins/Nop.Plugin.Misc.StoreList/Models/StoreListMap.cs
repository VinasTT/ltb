using System.Data.Entity.ModelConfiguration;

//NOP 3.828
namespace Nop.Plugin.Misc.StoreList.Models
{
    public class StoreListMap : EntityTypeConfiguration<StoreListRecord>
    {
        public StoreListMap()
        {
            ToTable("StoreList");

            HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.Name);
            Property(m => m.Published);
            Property(m => m.Description);
            Property(m => m.Address);
            Property(m => m.Contact);
            Property(m => m.Country);
            Property(m => m.StateProvince);
            Property(m => m.District);
            Property(m => m.Latitude);
            Property(m => m.Longitude);
            Property(m => m.StoreDealer);
           
        }
    }
}
