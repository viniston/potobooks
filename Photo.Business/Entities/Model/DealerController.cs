using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;

namespace Photo.Business.Entities.Model
{
    public class DealerController : ControllerBase<DealerInfo, int>
    {
        #region Singleton

        public static DealerController Instance { get; private set; }

        #endregion


        #region Constructor

        static DealerController()
        {
            Instance = new DealerController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<DealerInfo>> GetAllFunc => DataProviderManager.Provider.GetAllDealers;

        public DealerInfo GetByID(int id)
        {
            return All.Find(city => city.ID == id);
        }
    }
}
