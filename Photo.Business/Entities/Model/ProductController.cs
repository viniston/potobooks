using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo.Business.Entities.Model
{
    public class ProductController : ControllerBase<ProductInfo, int>
    {
        #region Singleton

        public static ProductController Instance { get; private set; }

        #endregion


        #region Constructor

        static ProductController()
        {
            Instance = new ProductController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<ProductInfo>> GetAllFunc => DataProviderManager.Provider.GetAllProducts;

        public ProductInfo GetByID(int id)
        {
            return All.Find(city => city.ID == id);
        }       
    }
}
