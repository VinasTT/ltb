using Nop.Core.Domain.Common;

namespace Nop.Data.Mapping.Common
{
    public partial class JeanGuideCategoryMap : NopEntityTypeConfiguration<JeanGuideCategory>
    {
        public JeanGuideCategoryMap()
        {
            this.ToTable("JeanGuideCategory");
            this.HasKey(aa => aa.Id);
            this.Property(aa => aa.Name).IsRequired().HasMaxLength(400);
            
        }
    }
}