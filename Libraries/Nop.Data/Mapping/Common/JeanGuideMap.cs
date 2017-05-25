using Nop.Core.Domain.Common;

namespace Nop.Data.Mapping.Common
{
    public partial class JeanGuideMap : NopEntityTypeConfiguration<JeanGuide>
    {
        public JeanGuideMap()
        {
            this.ToTable("JeanGuide");
            this.HasKey(aa => aa.Id);
            this.Property(aa => aa.Name).IsRequired().HasMaxLength(400);
            
        }
    }
}