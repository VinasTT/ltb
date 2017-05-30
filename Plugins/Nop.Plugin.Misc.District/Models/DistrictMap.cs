using System.Data.Entity.ModelConfiguration;

//NOP 3.828
namespace Nop.Plugin.Misc.District.Models
{
    public class DistrictMap : EntityTypeConfiguration<DistrictRecord>
    {
        public DistrictMap()
        {
            ToTable("District");

            HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.StateProvinceId);
            Property(m => m.Name);
            Property(m => m.Abbreviation);
            Property(m => m.DisplayOrder);
            Property(m => m.Published);
            Property(m => m.LongDistance);
        }
    }
}
