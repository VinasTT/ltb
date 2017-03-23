using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;

//NOP 3.82
namespace Nop.Data.Mapping.Catalog
{
    public partial class AdminFilterMap : NopEntityTypeConfiguration<AdminFilter>
    {
        public AdminFilterMap()
        {
            this.ToTable("AdminFilter");
            this.HasKey(p => p.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(400);
            this.Property(c => c.Published).IsRequired();
        }
    }
}
