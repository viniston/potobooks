using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Photo.Resources.RegEx;


namespace Photo.Business.Utilities.Formatting
{
	/// <summary>
	/// Helper class for formatting
	/// </summary>
	public static class FormatHelper
	{
		#region Private Members

		private const string DefaultLocalHostIp = "127.0.0.1";

		#endregion


		#region Public Methods


		#region General Formatting and Parsing

		/// <summary>
		/// Replaces non alphanumeric characters
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string CleanUpNonAlphaNumericCharacters(string text)
		{
			return Regex.Replace(text, RegEx.NonAlphanumerics, string.Empty);
		}

		/// <summary>
		/// Replace all non numeric characters
		/// </summary>
		/// <param name="text"></param>
		/// <returns>string</returns>
		public static string CleanUpNonNumericCharacters(string text)
		{
		    return Regex.Replace(text, RegEx.NonNumericCharacter, string.Empty);
		}

        /// <summary>
        /// Cleans Html tags
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public static string CleanUpHtmlTags(string text)
        {
            return Regex.Replace(text, RegEx.HTMLTags, string.Empty);
        }

        /// <summary>
        /// Cleans invalid character from password
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public static string CleanUpInvalidPasswordCharacters(string text)
        {
            return Regex.Replace(text, RegEx.InvalidPlusPasswordCharacters, string.Empty);
		}

		/// <summary>
		/// Cleans non Alpha character except space.
		/// </summary>
		/// <param name="text"></param>
		/// <returns>string</returns>
		public static string CleanUpNonAlphaCharactersAndKeepSpace(string text)
		{
		    return Regex.Replace(text, RegEx.NonAlphaWithSpaces, string.Empty);
		}

		/// <summary>
		/// Cleans non Alpha character except space and also remove redundant whitespaces
		/// </summary>
		/// <param name="text"></param>
		/// <returns>string</returns>
		public static string CleanUpNonAlphaCharactersAndKeepSingleSpaces(string text)
		{
			return CleanUpRedanduntWhiteSpaces(CleanUpNonAlphaCharactersAndKeepSpace(text));
		}

		/// <summary>
		/// Replaces all the redundant spaces in a character
		/// </summary>
		/// <param name="text"></param>
		/// <returns>string</returns>
		public static string CleanUpRedanduntWhiteSpaces(string text)
		{
		    return Regex.Replace(text, RegEx.RedundantWhitespaces, " ");
		}

		/// <summary>
		/// Converts long number to IP Address
		/// </summary>
		/// <param name="longIp"></param>
		/// <returns></returns>
		public static string ConvertLongToIpAddress(long longIp)
		{
			string ip = string.Empty;

			for (int i = 0; i < 4; i++)
			{
				int num = (int)(longIp / Math.Pow(256, (3 - i)));
				longIp = longIp - (long)(num * Math.Pow(256, (3 - i)));

				if (i == 0)
				{
					ip = num.ToString();
				}
				else
				{
					ip = ip + "." + num;
				}
			}
			return ip;
		}

		/// <summary>
		/// Converts the IP address string into its long equivalent value
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <returns>long</returns>
		public static long GetIpAddressValue(string ipAddress)
		{
		    var requestClientIp = IPAddress.Parse(ipAddress == "::1" ? DefaultLocalHostIp : ipAddress);

		    long ipNum = 0;
			byte[] b = requestClientIp.GetAddressBytes();

			for (var i = 0; i < 4; ++i)
			{
				long y = b[i];
				if (y < 0)
					y += 256;

				ipNum += y << ((3 - i) * 8);
			}

			return ipNum;
		}

		/// <summary>
		/// CreateDatetime is a helping method to create datetime value from passed string datetime type
		/// </summary>
		/// <param name="strDate"></param>
		/// <returns>DateTime</returns>
		public static DateTime CreateDatetime(string strDate)
		{
			DateTime dt;
			string format;
			IFormatProvider provider = CultureInfo.InvariantCulture;

			try
			{
				format = @"d/M/yyyy";
				dt = DateTime.ParseExact(strDate, format, provider);
				if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
					return dt;
			}
			catch
			{
				try
				{
					format = @"d/M/yy";
					dt = DateTime.ParseExact(strDate, format, provider);
					if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
						return dt;
				}
				catch
				{
					try
					{
						format = @"d/M/y";
						dt = DateTime.ParseExact(strDate, format, provider);
						if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
							return dt;
					}
					catch
					{
						format = @"yyyy-MM-dd";
						dt = DateTime.ParseExact(strDate, format, provider);
						if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
							return dt;
					}
				}
			}
			return new DateTime();
		}

        /// <summary>
		/// Replaces all the occurence of non alphanumeric characters with a dash
		/// </summary>
		/// <param name="text"></param>
		/// <returns>string</returns>
		public static string CleanUpNonAlphaNumericCharachtersWithDash(string text)
        {
            return Regex.Replace(text, RegEx.NonAlphaNumericCharacters, "-").Trim();
        }

        #endregion

        #endregion
    }
}