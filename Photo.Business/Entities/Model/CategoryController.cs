using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;

namespace Photo.Business.Entities.Model
{
    public class CategoryController : ControllerBase<CategoryInfo, int>
    {
        #region Singleton

        public static CategoryController Instance { get; private set; }

        #endregion


        #region Constructor

        static CategoryController()
        {
            Instance = new CategoryController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<CategoryInfo>> GetAllFunc => DataProviderManager.Provider.GetAllCategories;

        public CategoryInfo GetByID(int id)
        {
            return All.Find(city => city.ID == id);
        }
    }
}
