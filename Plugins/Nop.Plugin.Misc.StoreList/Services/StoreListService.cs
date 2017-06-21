using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Services.Events;
using Nop.Core.Domain.Common;
using Nop.Plugin.Misc.StoreList.Models;
//NOP 3.828
namespace Nop.Plugin.Misc.StoreList.Services
{
    /// <summary>
    /// State province service
    /// </summary>
    public partial class StoreListService : IStoreListService
    {

        #region Fields

        private readonly IRepository<StoreListRecord> _storeListRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="DistrictRepository">State/province repository</param>
        /// <param name="eventPublisher">Event published</param>
        public StoreListService(ICacheManager cacheManager,
            IRepository<StoreListRecord> storeListRepository,
            IEventPublisher eventPublisher)
        {
            _cacheManager = cacheManager;
            _storeListRepository = storeListRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Deletes a state/province
        /// </summary>
        /// <param name="District">The state/province</param>
        public virtual void DeleteStoreList(StoreListRecord storeList)
        {
            if (storeList == null)
                throw new ArgumentNullException("StoreList");

            _storeListRepository.Delete(storeList);
            
        }

        /// <summary>
        /// Gets a state/province
        /// </summary>
        /// <param name="DistrictId">The state/province identifier</param>
        /// <returns>State/province</returns>
        public virtual StoreListRecord GetStoreListById(int storeListId)
        {
            if (storeListId == 0)
                return null;

            return _storeListRepository.GetById(storeListId);
        }


        /// <summary>
        /// Gets all states/provinces
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        public virtual IList<StoreListRecord> GetStoreList(bool showHidden = false)
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where showHidden || sp.Published
                        select sp;
            var StoreList = query.ToList();
            return StoreList;
        }

        public virtual IList<string> GetCountryList()
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where  sp.Published
                        select sp.Country;
            query = query.Distinct();
            var CountryList = query.ToList();
            return CountryList;
        }

        public virtual IList<string> GetStateProvinceList()
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where sp.Published
                        select sp.StateProvince;
            query = query.Distinct();
            var StateProvinceList = query.ToList();
            return StateProvinceList;
        }

        public virtual IList<string> GetStateProvinceListByCountry(string country)
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where sp.Published && sp.Country == country
                        select sp.StateProvince;
            query = query.Distinct();
            var StateProvinceList = query.ToList();
            return StateProvinceList;
        }

        public virtual IList<string> GetDistrictList()
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where sp.Published
                        select sp.District;
            query = query.Distinct();
            var DistrictList = query.ToList();
            return DistrictList;
        }

        public virtual IList<string> GetDistrictListByStateProvince(string country , string stateProvince)
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where sp.Published && sp.StateProvince == stateProvince && sp.Country == country
                        select sp.District;
            query = query.Distinct();
            var DistrictList = query.ToList();
            return DistrictList;
        }

        public virtual IList<StoreListRecord> GetStoreListByCountry(string country)
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where sp.Published && sp.Country == country
                        select sp;
            var StoreList = query.ToList();
            return StoreList;

        }

        public virtual IList<StoreListRecord> GetStoreListByStateProvince(string stateProvince)
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where sp.Published && sp.StateProvince == stateProvince
                        select sp;
            var StoreList = query.ToList();
            return StoreList;

        }

        public virtual StoreListRecord GetStoreByDistrict(string district)
        {
            var query = from sp in _storeListRepository.Table
                        orderby sp.Country, sp.StateProvince, sp.District
                        where sp.Published && sp.District == district
                        select sp;
            var StoreList = query.FirstOrDefault();
            return StoreList;

        }

        /// <summary>
        /// Inserts a state/province
        /// </summary>
        /// <param name="District">State/province</param>
        public virtual void InsertStoreList(StoreListRecord storeList)
        {
            if (storeList == null)
                throw new ArgumentNullException("StoreList");

            _storeListRepository.Insert(storeList);
            
        }

        /// <summary>
        /// Updates a state/province
        /// </summary>
        /// <param name="District">State/province</param>
        public virtual void UpdateStoreList(StoreListRecord storeList)
        {
            if (storeList == null)
                throw new ArgumentNullException("StoreList");

            _storeListRepository.Update(storeList);
            
            
        }

        

        #endregion
    }
}
