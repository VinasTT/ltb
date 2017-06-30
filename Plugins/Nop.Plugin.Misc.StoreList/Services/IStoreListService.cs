using System.Collections.Generic;
using Nop.Plugin.Misc.StoreList.Models;
using Nop.Core.Domain.Common;
//NOP 3.828
namespace Nop.Plugin.Misc.StoreList.Services
{
    /// <summary>
    /// State province service interface
    /// </summary>
    public partial interface IStoreListService
    {
        /// <summary>
        /// Deletes a state/province
        /// </summary>
        /// <param name="stateProvince">The state/province</param>
        void DeleteStoreList(StoreListRecord storeList);

        /// <summary>
        /// Gets a state/province
        /// </summary>
        /// <param name="stateProvinceId">The state/province identifier</param>
        /// <returns>State/province</returns>
        StoreListRecord GetStoreListById(int storeListId);

        /// <summary>
        /// Gets all states/provinces
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>States</returns>
        IList<StoreListRecord> GetStoreList(bool showHidden = false);
        IList<string> GetCountryList();

        IList<string> GetStateProvinceList();

        IList<string> GetStateProvinceListByCountry(string country);

        IList<string> GetDistrictList();

        IList<string> GetDistrictListByStateProvince(string country , string stateProvince);

        IList<StoreListRecord> GetStoreListByCountry(string country);

        IList<StoreListRecord> GetStoreListByStateProvince(string stateProvince);

        IList<StoreListRecord> GetStoreListByStoreDealer(string storeDealer);

        StoreListRecord GetStoreByDistrict(string district);

        /// <summary>
        /// Inserts a state/province
        /// </summary>
        /// <param name="stateProvince">State/province</param>
        void InsertStoreList(StoreListRecord sotreList);

        /// <summary>
        /// Updates a state/province
        /// </summary>
        /// <param name="stateProvince">State/province</param>
        void UpdateStoreList(StoreListRecord storeList);


    }
}
