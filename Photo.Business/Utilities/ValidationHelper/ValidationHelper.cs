using System;
using Photo.Business.Utilities.Formatting;

namespace Photo.Business.Utilities.ValidationHelper
{
	public static class ValidationHelper
	{
		#region Private Methods

		/// <summary>
		/// Check if decimal have valid digits
		/// </summary>
		/// <param name="number">string</param>
		/// <param name="maxDecimalDigits">byte</param>
		/// <returns>bool</returns>
		private static bool IsValidDecimalDigits(string number, byte maxDecimalDigits)
		{
			return (number.IndexOf('.') >= 0 ? number.Substring(number.IndexOf('.') + 1).Length : 0) <= maxDecimalDigits;
		}

		#endregion


		#region Public Members

		/// <summary>
		/// Verify if number passed is a valid double type or not
		/// </summary>
		/// <param name="number">The number.</param>
		/// <returns>bool</returns>
		public static bool IsDecimal(string number)
		{
			return IsValidDecimal(number, null, null);
		}

		/// <summary>
		/// Verify if passed decimal number's fraction part does not exceeds the maxDecimalDigit
		/// </summary>
		/// <param name="number">The number.</param>
		/// <param name="maxDecimalDigits">The maximum decimal digits.</param>
		/// <returns>bool</returns>
		public static bool IsDecimal(string number, byte maxDecimalDigits)
		{
			return IsValidDecimal(number, null, null) && IsValidDecimalDigits(number, maxDecimalDigits);
		}

		/// <summary>
		/// Method will check if number is decimal and positive
		/// </summary>
		/// <param name="number">The number.</param>
		/// <returns>bool</returns>
		public static bool IsPositiveDecimal(string number)
		{
			return IsValidDecimal(number, 0, null);
		}

		/// <summary>
		/// Method will check if number is decimal and positive
		/// </summary>
		/// <param name="number">The number.</param>
		/// <param name="maxDecimalDigits">The maximum decimal digits.</param>
		/// <returns>bool</returns>
		public static bool IsPositiveDecimal(string number, byte maxDecimalDigits)
		{
			return IsValidDecimal(number, 0, null) && IsValidDecimalDigits(number, maxDecimalDigits);
		}

		/// <summary>
		/// Check if the given string is a valid decimal within the gievn range
		/// </summary>
		/// <param name="number">String value to parsed as decimal and checked</param>
		/// <param name="minValue">Minimum valid value. If 'null' is passed then minimum is not checked</param>
		/// <param name="maxValue">Maximum valid value. If 'null' is passed then maximum is not checked</param>
		/// <returns>boolean value indicating if the given string represent a valid decimal for the given min and max values</returns>
		public static bool IsValidDecimal(string number, decimal? minValue, decimal? maxValue)
		{
			if (string.IsNullOrEmpty(number))
				return false;

			string numberCopy = number.Trim();
			if (string.IsNullOrEmpty(numberCopy))
				return false;

			decimal value;
			if (!decimal.TryParse(number, out value))
				return false;

			if ((minValue.HasValue && value < minValue.Value) || (maxValue.HasValue && value > maxValue.Value))
				return false;

			return true;
		}

		/// <summary>
		/// IsSmallDate is a helping method used to verify if date passed is valid date between 01/0/1900 - 06/06/2079 or not, if yes then 
		/// returns true else false
		/// </summary>
		/// <param name="strDate">The string date.</param>
		/// <returns>bool</returns>
		public static bool IsSmallDate(string strDate)
		{
			try
			{
				DateTime minSmallDateTime = new DateTime(1900, 01, 01, 00, 00, 00);
				DateTime maxSmallDateTime = new DateTime(2079, 06, 06, 23, 59, 59);

				DateTime dt = FormatHelper.CreateDatetime(strDate);
				if (dt != minSmallDateTime && dt != maxSmallDateTime && dt >= minSmallDateTime && dt <= maxSmallDateTime)
					return true;

				return false;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Returns true if the first date is between the second and the third date (Inclusive of the limits)
		/// </summary>
		/// <param name="date">The date.</param>
		/// <param name="laterThan">The later than.</param>
		/// <param name="earlierThan">The earlier than.</param>
		/// <returns><c>true</c> if [is date between] [the specified date]; otherwise, <c>false</c>.</returns>
		public static bool IsDateBetween(DateTime date, DateTime laterThan, DateTime earlierThan)
		{
			return (date >= laterThan && date <= earlierThan);
		}

		/// <summary>
		/// Validate the GUID
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public static bool IsValidGUID(string guid)
		{
			bool isValidGuid = false;

			if (!string.IsNullOrEmpty(guid))
			{
				try
				{
					new Guid(guid);
					isValidGuid = true;
				}
				catch (Exception)
				{
				}
			}
			return isValidGuid;
		}

		#endregion
	}
}
