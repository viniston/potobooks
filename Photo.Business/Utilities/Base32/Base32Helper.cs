using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo.Business.Utilities.Base32
{
	/// <summary>
	/// Utility for converting Base32 string values to long, and vice versa
	/// </summary>
	public class Base32Helper
	{
		/// <summary>
		/// 1 and i are removed since they can be confusing, while L is left since it will be always represented only in caps, 
		/// so it won't be as ambiguous. Also both 0 and O are removed.
		/// </summary>
		private static string Base32Chars = "23456789ABCDEFGHJLKMNPQRSTUVWXYZ";

		/// <summary>
		/// Converts from a numeric long value to its corresponding base32 string representation, based on our custom base32 characters
		/// </summary>
		/// <param name="base10"></param>
		/// <returns>string</returns>
		public static string ToBase32(long base10)
		{
			string result = "";
			long remainder;

			do
			{
				remainder = base10 % 32;
				base10 = base10 / 32;
				result = Base32Chars[(int)remainder] + result;

			} while (base10 > 0);

			return result;
		}

		/// <summary>
		/// Converts back from a base32 string representation to its numeric long value, based on our custom base32 characters
		/// </summary>
		/// <param name="base32"></param>
		/// <returns>long</returns>
		public static long FromBase32(string base32)
		{
			base32 = base32.Trim();

			int result = 0;

			for (int i = 0; i < base32.Length; i++)
			{
				result *= 32;
				result += Base32Chars.IndexOf(base32[i]);
			}

			return result;
		}
	}
}
