using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Services.Events;
using Nop.Core.Domain.Common;
using Nop.Plugin.Misc.District.Models;
//NOP 3.828
namespace Nop.Plugin.Misc.District.Services
{
    /// <summary>
    /// State province service
    /// </summary>
    public partial class DistrictService : IDistrictService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : country ID
        /// {1} : language ID
        /// {2} : show hidden records?
        /// </remarks>
        private const string DistrictS_ALL_KEY = "Nop.District.all-{0}-{1}-{2}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string DistrictS_PATTERN_KEY = "Nop.District.";

        #endregion

        #region Fields

        private readonly IRepository<DistrictRecord> _districtRepository;
        private readonly IRepository<StateProvince> _stateRepository;
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
        public DistrictService(ICacheManager cacheManager,
            IRepository<DistrictRecord> districtRepository,
            IRepository<StateProvince> stateRepository,
            IEventPublisher eventPublisher)
        {
            _cacheManager = cacheManager;
            _districtRepository = districtRepository;
            _stateRepository = stateRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Deletes a state/province
        /// </summary>
        /// <param name="District">The state/province</param>
        public virtual void DeleteDistrict(DistrictRecord district)
        {
            if (district == null)
                throw new ArgumentNullException("District");

            _districtRepository.Delete(district);

            _cacheManager.RemoveByPattern(DistrictS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(district);
        }

        /// <summary>
        /// Gets a state/province
        /// </summary>
        /// <param name="DistrictId">The state/province identifier</param>
        /// <returns>State/province</returns>
        public virtual DistrictRecord GetDistrictById(int districtId)
        {
            if (districtId == 0)
                return null;

            return _districtRepository.GetById(districtId);
        }

        /// <summary>
        /// Gets a state/province 
        /// </summary>
        /// <param name="abbreviation">The state/province abbreviation</param>
        /// <returns>State/province</returns>
        public virtual DistrictRecord GetDistrictByAbbreviation(string abbreviation)
        {
            var query = from sp in _districtRepository.Table
                        where sp.Abbreviation == abbreviation
                        select sp;
            var District = query.FirstOrDefault();
            return District;
        }

        /// <summary>
        /// Gets a state/province collection by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="languageId">Language identifier. It's used to sort states by localized names (if specified); pass 0 to skip it</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        public virtual IList<DistrictRecord> GetDistrictsByStateProvinceId(int stateProvinceId, int languageId = 0, bool showHidden = false)
        {
            
                var query = from sp in _districtRepository.Table
                            orderby sp.DisplayOrder, sp.Name
                            where sp.StateProvinceId == stateProvinceId &&
                            (showHidden || sp.Published)
                            select sp;
                var districts = query.ToList();

                if (languageId > 0)
                {
                    //we should sort states by localized names when they have the same display order
                    districts = districts
                        .OrderBy(c => c.DisplayOrder)
                        .ToList();
                }
                return districts;
           
        }

        /// <summary>
        /// Gets all states/provinces
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        public virtual IList<DistrictRecord> GetDistricts(bool showHidden = false)
        {
            var query = from sp in _districtRepository.Table
                        orderby sp.StateProvinceId, sp.DisplayOrder, sp.Name
                        where showHidden || sp.Published
                        select sp;
            var Districts = query.ToList();
            return Districts;
        }

        /// <summary>
        /// Inserts a state/province
        /// </summary>
        /// <param name="District">State/province</param>
        public virtual void InsertDistrict(DistrictRecord district)
        {
            if (district == null)
                throw new ArgumentNullException("District");

            _districtRepository.Insert(district);
            
        }

        /// <summary>
        /// Updates a state/province
        /// </summary>
        /// <param name="District">State/province</param>
        public virtual void UpdateDistrict(DistrictRecord district)
        {
            if (district == null)
                throw new ArgumentNullException("District");

            _districtRepository.Update(district);
            
            
        }

        public bool CheckIfLongDistance(Address address)
        {
          
            var query = from c in _districtRepository.Table
                        orderby c.Id
                        where c.Id == address.DistrictId
                        && c.LongDistance == true
                        select c;
            var record = query.FirstOrDefault();

            if (record != null)
                return true;
            else
                return false;
        }

        public int GetStateProvinceMappingId(int? districtId)
        {
            var query = from sp in _districtRepository.Table
                        where sp.Id == districtId
                        select sp;
            var State = query.FirstOrDefault();
            if (State != null)
                return State.StateProvinceId;
            else
                return 0;
        }

        public int GetStateProvinceMappingIdByStateId(int? stateId)
        {
            var query = from sp in _stateRepository.Table
                        where sp.Id == stateId
                        select sp;
            var State = query.FirstOrDefault();
            if (State != null)
                return State.StateProvinceId;
            else
                return 0;
        }

        #endregion
    }
}
