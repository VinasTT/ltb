using System.Collections.Generic;
using Nop.Plugin.Misc.District.Models;
using Nop.Core.Domain.Common;
//NOP 3.828
namespace Nop.Plugin.Misc.District.Services
{
    /// <summary>
    /// State province service interface
    /// </summary>
    public partial interface IDistrictService
    {
        /// <summary>
        /// Deletes a state/province
        /// </summary>
        /// <param name="stateProvince">The state/province</param>
        void DeleteDistrict(DistrictRecord district);

        /// <summary>
        /// Gets a state/province
        /// </summary>
        /// <param name="stateProvinceId">The state/province identifier</param>
        /// <returns>State/province</returns>
        DistrictRecord GetDistrictById(int stateProvinceId);

        /// <summary>
        /// Gets a state/province 
        /// </summary>
        /// <param name="abbreviation">The state/province abbreviation</param>
        /// <returns>State/province</returns>
        DistrictRecord GetDistrictByAbbreviation(string abbreviation);

        /// <summary>
        /// Gets a state/province collection by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="languageId">Language identifier. It's used to sort states by localized names (if specified); pass 0 to skip it</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        IList<DistrictRecord> GetDistrictsByStateProvinceId(int stateProvinceId, int languageId = 0, bool showHidden = false);

        /// <summary>
        /// Gets all states/provinces
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        IList<DistrictRecord> GetDistricts(bool showHidden = false);

        /// <summary>
        /// Inserts a state/province
        /// </summary>
        /// <param name="stateProvince">State/province</param>
        void InsertDistrict(DistrictRecord district);

        /// <summary>
        /// Updates a state/province
        /// </summary>
        /// <param name="stateProvince">State/province</param>
        void UpdateDistrict(DistrictRecord district);

        bool CheckIfLongDistance(Address address);

        int GetStateProvinceMappingId(int? districtId);

        int GetStateProvinceMappingIdByStateId(int? stateId);
    }
}
