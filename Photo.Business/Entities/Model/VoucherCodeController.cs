using System;
using System.Linq;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System.Collections.Generic;

namespace Photo.Business.Entities.Model
{
    public class VoucherCodeController : ControllerBase<VoucherCodeInfo, long>
    {
        #region Singleton

        public static VoucherCodeController Instance { get; private set; }

        #endregion


        #region Constructor

        static VoucherCodeController()
        {
            Instance = new VoucherCodeController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<VoucherCodeInfo>> GetAllFunc => DataProviderManager.Provider.GetAllVoucherCodes;

        public VoucherCodeInfo GetByID(long id)
        {
            return All.Find(voucher => voucher.ID == id);
        }

		public VoucherCodeInfo CheckVoucherCodeByCodeAndDealer(string voucherCode, int dealerID)
		{
			return DataProviderManager.Provider.CheckVoucherCodeByCodeAndDealer(voucherCode, dealerID).FirstOrDefault();
		}

		public bool RedeemVoucherCode(long voucherID, string securityCode)
		{
			return DataProviderManager.Provider.RedeemVoucherCode(voucherID, securityCode);
		}
	}
}
