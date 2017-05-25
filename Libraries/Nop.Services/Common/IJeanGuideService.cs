using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Catalog;
//NOP 3.832
namespace Nop.Services.Common
{
    /// <summary>
    /// Address attribute service
    /// </summary>
    public partial interface IJeanGuideService
    {
        /// <summary>
        /// Deletes an address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        void DeleteJeanGuide(JeanGuide jeanGuide);

        /// <summary>
        /// Gets all address attributes
        /// </summary>
        /// <returns>Address attributes</returns>
        IList<JeanGuide> GetAllJeanGuides();

        IList<JeanGuide> GetJeanGuidesByCategoryId(int jeanGuideCategoryId);

        /// <summary>
        /// Gets an address attribute 
        /// </summary>
        /// <param name="addressAttributeId">Address attribute identifier</param>
        /// <returns>Address attribute</returns>
        JeanGuide GetJeanGuideById(int jeanGuideId);

        /// <summary>
        /// Inserts an address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        void InsertJeanGuide(JeanGuide jeanGuide);

        /// <summary>
        /// Updates the address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        void UpdateJeanGuide(JeanGuide jeanGuide);

        /// <summary>
        /// Deletes an address attribute value
        /// </summary>
        /// <param name="addressAttributeValue">Address attribute value</param>
        void DeleteJeanGuideCategory(JeanGuideCategory jeanGuideCategory);

        /// <summary>
        /// Gets address attribute values by address attribute identifier
        /// </summary>
        /// <param name="addressAttributeId">The address attribute identifier</param>
        /// <returns>Address attribute values</returns>
        IList<JeanGuideCategory> GetJeanGuideCategoriesByGenre(string genre);

        /// <summary>
        /// Gets an address attribute value
        /// </summary>
        /// <param name="addressAttributeValueId">Address attribute value identifier</param>
        /// <returns>Address attribute value</returns>
        JeanGuideCategory GetJeanGuideCategoryById(int jeanGuideCategoryId);

        /// <summary>
        /// Gets an address attribute value
        /// </summary>
        /// <param name="addressAttributeValueId">Address attribute value identifier</param>
        /// <returns>Address attribute value</returns>
        int GetJeanGuideNopCategoryIdByName(string jeanGuideCategoryName);

        /// <summary>
        /// Inserts a ddress attribute value
        /// </summary>
        /// <param name="addressAttributeValue">Address attribute value</param>
        void InsertJeanGuideCategory(JeanGuideCategory jeanGuideCategory);

        /// <summary>
        /// Updates the ddress attribute value
        /// </summary>
        /// <param name="addressAttributeValue">Address attribute value</param>
        void UpdateJeanGuideCategory(JeanGuideCategory jeanGuideCategory);
    }
}
