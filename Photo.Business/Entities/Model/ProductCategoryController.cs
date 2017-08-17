using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;

namespace Photo.Business.Entities.Model
{
    public class ProductCategoryController : ControllerBase<ProductCategoryInfo, int>
    {
        #region Singleton

        public static ProductCategoryController Instance { get; private set; }

        #endregion


        #region Constructor

        static ProductCategoryController()
        {
            Instance = new ProductCategoryController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<ProductCategoryInfo>> GetAllFunc => DataProviderManager.Provider.GetAllProductCategories;

        public ProductCategoryInfo GetByID(int id)
        {
            return All.Find(city => city.ID == id);
        }
    }
}
