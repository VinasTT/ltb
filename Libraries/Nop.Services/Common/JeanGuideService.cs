using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Services.Events;
using Nop.Core.Domain.Catalog;
//NOP 3.832
namespace Nop.Services.Common
{
    /// <summary>
    /// Address attribute service
    /// </summary>
    public partial class JeanGuideService : IJeanGuideService
    {
        
        
        #region Fields

        private readonly IRepository<JeanGuideCategory> _jeanGuideCategoryRepository;
        private readonly IRepository<JeanGuide> _jeanGuideRepository;
        private readonly IRepository<Category> _categoryRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="jeanGuideCategoryRepository">Address attribute repository</param>
        /// <param name="jeanGuideRepository">Address attribute value repository</param>
        public JeanGuideService(IRepository<JeanGuideCategory> jeanGuideCategoryRepository,
            IRepository<JeanGuide> jeanGuideRepository,
            IRepository<Category> categoryRepository)
        {
            this._jeanGuideCategoryRepository = jeanGuideCategoryRepository;
            this._jeanGuideRepository = jeanGuideRepository;
            this._categoryRepository = categoryRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes an address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        public virtual void DeleteJeanGuide(JeanGuide jeanGuide)
        {
            if (jeanGuide == null)
                throw new ArgumentNullException("jeanGuide");

            _jeanGuideRepository.Delete(jeanGuide);
            
           
        }

        /// <summary>
        /// Gets all address attributes
        /// </summary>
        /// <returns>Address attributes</returns>
        public virtual IList<JeanGuide> GetAllJeanGuides()
        {
            
                var query = from aa in _jeanGuideRepository.Table
                            orderby aa.Name
                            select aa;
                return query.ToList();
            
        }

        public IList<JeanGuide> GetJeanGuidesByCategoryId(int jeanGuideCategoryId)
        {
            var query = from aa in _jeanGuideRepository.Table
                        orderby aa.Name
                        where aa.JeanGuideCategoryId == jeanGuideCategoryId
                        select aa;
            return query.ToList();
        }

        /// <summary>
        /// Gets an address attribute 
        /// </summary>
        /// <param name="addressAttributeId">Address attribute identifier</param>
        /// <returns>Address attribute</returns>
        public virtual JeanGuide GetJeanGuideById(int jeanGuideId)
        {
            if (jeanGuideId == 0)
                return null;
            
            return _jeanGuideRepository.GetById(jeanGuideId);
        }

        /// <summary>
        /// Inserts an address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        public virtual void InsertJeanGuide(JeanGuide jeanGuide)
        {
            if (jeanGuide == null)
                throw new ArgumentNullException("jeanGuide");

            _jeanGuideRepository.Insert(jeanGuide);
            
        }

        /// <summary>
        /// Updates the address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        public virtual void UpdateJeanGuide(JeanGuide jeanGuide)
        {
            if (jeanGuide == null)
                throw new ArgumentNullException("jeanGuide");

            _jeanGuideRepository.Update(jeanGuide);
        }

        /// <summary>
        /// Deletes an address attribute value
        /// </summary>
        /// <param name="addressAttributeValue">Address attribute value</param>
        public virtual void DeleteJeanGuideCategory(JeanGuideCategory jeanGuideCategory)
        {
            if (jeanGuideCategory == null)
                throw new ArgumentNullException("jeanGuideCategory");

            _jeanGuideCategoryRepository.Delete(jeanGuideCategory);
        }

        /// <summary>
        /// Gets address attribute values by address attribute identifier
        /// </summary>
        /// <param name="addressAttributeId">The address attribute identifier</param>
        /// <returns>Address attribute values</returns>
        public virtual IList<JeanGuideCategory> GetJeanGuideCategoriesByGenre(string genre)
        {
           
                var query = from aav in _jeanGuideCategoryRepository.Table
                            orderby aav.Name
                            where aav.Genre == genre
                            select aav;
                var jeanGuideCategories = query.ToList();
                return jeanGuideCategories;
        
        }

        /// <summary>
        /// Gets an jean guide category by id
        /// </summary>
        /// <param name="jeanGuideCategoryId">Jean guide category identifier</param>
        /// <returns>Address attribute value</returns>
        public virtual JeanGuideCategory GetJeanGuideCategoryById(int jeanGuideCategoryId)
        {
            if (jeanGuideCategoryId == 0)
                return null;
            
            return _jeanGuideCategoryRepository.GetById(jeanGuideCategoryId);
        }

        public virtual int GetJeanGuideNopCategoryIdByName(string jeanGuideCategoryName)
        {
            var query = from aav in _categoryRepository.Table
                        where aav.Name == jeanGuideCategoryName
                        select aav.Id;
            var categoryId = query.FirstOrDefault();
            return categoryId;
        }

        /// <summary>
        /// Inserts an jean guide category
        /// </summary>
        /// <param name="jeanGuideCategory">Jean guide category</param>
        public virtual void InsertJeanGuideCategory(JeanGuideCategory jeanGuideCategory)
        {
            if (jeanGuideCategory == null)
                throw new ArgumentNullException("jeanGuideCategory");

            _jeanGuideCategoryRepository.Insert(jeanGuideCategory);
            
        }

        /// <summary>
        /// Updates the Jean guide category
        /// </summary>
        /// <param name="jeanGuideCategory">Jean guide category</param>
        public virtual void UpdateJeanGuideCategory(JeanGuideCategory jeanGuideCategory)
        {
            if (jeanGuideCategory == null)
                throw new ArgumentNullException("jeanGuideCategory");

            _jeanGuideCategoryRepository.Update(jeanGuideCategory);
            
        }
        
        #endregion
    }
}
