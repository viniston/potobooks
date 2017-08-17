using Photo.Business.Entities.Model.Common.Money;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace Photo.Business.Entities.Model.Common.Currency
{
	/// <summary>
	/// The Manager class for Currency-related functions
	/// </summary>
	public static class CurrencyManager
	{
		#region Private Members

		private static Dictionary<string, decimal> _currency_ConversionRateDictionary = new Dictionary<string, decimal>();
		private static ReaderWriterLock rwlock = new ReaderWriterLock();

		#endregion


		#region Private Methods

		private static decimal GetCurrencyConversionRatio(CurrencyInfo originalCurrency, CurrencyInfo targetCurrency)
		{
			rwlock.AcquireReaderLock(Timeout.Infinite);
			try
			{
				string currencyConversionKey = originalCurrency.ISOCode3 + "To" + targetCurrency.ISOCode3 + "ConversionRate";

				if (!_currency_ConversionRateDictionary.ContainsKey(currencyConversionKey))
				{
					decimal conversionRate;
					if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[currencyConversionKey])
						&& decimal.TryParse(ConfigurationManager.AppSettings[currencyConversionKey], out conversionRate))
					{
						LockCookie lc = rwlock.UpgradeToWriterLock(Timeout.Infinite);
						try
						{
							if (!_currency_ConversionRateDictionary.ContainsKey(currencyConversionKey))
							{
								_currency_ConversionRateDictionary.Add(currencyConversionKey, conversionRate);
							}
							return _currency_ConversionRateDictionary[currencyConversionKey];

						}
						finally
						{
							rwlock.DowngradeFromWriterLock(ref lc);
						}
					}
					else
						throw new Exception("Conversion between " + originalCurrency.ISOCode3 + " and " + targetCurrency.ISOCode3
							+ " is not currently available");
				}
				else
				{
					return _currency_ConversionRateDictionary[currencyConversionKey];
				}
			}
			finally
			{
				rwlock.ReleaseReaderLock();
			}
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Converts the provided amount to its equivalent in the provided currency
		/// </summary>
		/// <param name="originalAmount"></param>
		/// <param name="targetCurrency"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo ConvertAmount(MoneyInfo originalAmount, CurrencyInfo targetCurrency)
		{
			return ConvertAmount(originalAmount, targetCurrency, DateTime.UtcNow, true, GetCurrencyConversionRatio(originalAmount.Currency, targetCurrency));
		}

		/// <summary>
		/// Converts the provided amount to its equivalent in the provided currency
		/// </summary>
		/// <param name="originalAmount"></param>
		/// <param name="targetCurrency"></param>
		/// <param name="roundValue"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo ConvertAmount(MoneyInfo originalAmount, CurrencyInfo targetCurrency, bool roundValue)
		{
			return ConvertAmount(originalAmount, targetCurrency, DateTime.UtcNow, roundValue, GetCurrencyConversionRatio(originalAmount.Currency, targetCurrency));
		}

		/// <summary>
		/// Converts the provided amount to its equivalent in the provided currency, and conversion rate
		/// </summary>
		/// <param name="originalAmount"></param>
		/// <param name="targetCurrency"></param>
		/// <param name="currencyConversionRatio"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo ConvertAmount(MoneyInfo originalAmount, CurrencyInfo targetCurrency, decimal currencyConversionRatio)
		{
			return ConvertAmount(originalAmount, targetCurrency, DateTime.UtcNow, true, currencyConversionRatio);
		}

		/// <summary>
		/// Converts the provided amount to its equivalent in the provided currency
		/// </summary>
		/// <param name="originalAmount"></param>
		/// <param name="targetCurrency"></param>
		/// <param name="conversionDate"></param>
		/// <param name="roundValue"></param>
		/// <param name="currencyConversionRatio"></param>
		/// <returns>MoneyInfo</returns>
		public static MoneyInfo ConvertAmount(MoneyInfo originalAmount, CurrencyInfo targetCurrency, DateTime conversionDate, bool roundValue, decimal currencyConversionRatio)
		{
			MoneyInfo result = new MoneyInfo(originalAmount.Value * currencyConversionRatio, targetCurrency);

			if (roundValue)
				result.RoundValue();

			return result;
		}

		#endregion
	}
}