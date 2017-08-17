using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo.Business.Entities.Model
{
    public class PaymentController : ControllerBase<PaymentInfo, long>
    {
        #region Singleton

        public static PaymentController Instance { get; private set; }

        #endregion


        #region Constructor

        static PaymentController()
        {
            Instance = new PaymentController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<PaymentInfo>> GetAllFunc => DataProviderManager.Provider.GetAllPayments;

        public PaymentInfo GetByID(long id)
        {
            return All.Find(city => city.ID == id);
        }
    }
}
