using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
//NOP 3.82
namespace Nop.Services.Catalog
{
    public partial interface IAdminFilterService
    {
        IList<AdminFilter> GetAllFilters();
    }
}
