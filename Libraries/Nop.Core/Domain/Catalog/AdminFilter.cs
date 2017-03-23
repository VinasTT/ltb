using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NOP 3.82
namespace Nop.Core.Domain.Catalog
{
    public partial class AdminFilter : BaseEntity
    {
        public string Name { get; set; }
        public bool Published { get; set; }
    }
}
