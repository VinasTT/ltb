using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;

//NOP 3.82
namespace Nop.Services.Catalog
{
    public partial class AdminFilterService : IAdminFilterService
    {
        private readonly IRepository<AdminFilter> _filterRepository;

        public AdminFilterService(IRepository<AdminFilter> filterRepository)
        {
            _filterRepository = filterRepository;
        }

        public virtual IList<AdminFilter> GetAllFilters()
        {
            var query = from p in _filterRepository.Table
                        orderby p.Id
                        where p.Published
                        select p;
            var filters = query.ToList();
            return filters;
        }
    }
}
