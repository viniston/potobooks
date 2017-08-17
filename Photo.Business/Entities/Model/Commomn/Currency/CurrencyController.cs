using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using Photo.Business.Entities.Model.Common.Currency;
using System;
using System.Collections.Generic;

namespace Photo.Business.Entities.Model
{
	public class CurrencyController : ControllerBase<CurrencyInfo, short>
	{
		#region Singleton

		public static CurrencyController Instance { get; private set; }

		#endregion


		#region Constructor

		static CurrencyController()
		{
			Instance = new CurrencyController();
		}

		#endregion

		protected override Func<DateTime?, IEnumerable<CurrencyInfo>> GetAllFunc => DataProviderManager.Provider.GetAllCurrencies;

		public CurrencyInfo GetByID(short id)
		{
			return All.Find(city => city.ID == id);
		}

		public CurrencyInfo GetByISOCode3(string code)
		{
			return All.Find(item => item.ISOCode3 == code.ToUpper());
		}

		public CurrencyInfo GetByISOCodeNumeric(string code)
		{
			return All.Find(item => item.ISOCodeNumeric == code.ToUpper());
		}
	}
}
