using System.Text.RegularExpressions;
using Photo.Resources.RegEx;

namespace Photo.Utility.Validation
{
	public static class ValidationHelper
	{
		#region Public Methods


		/// <summary>
		/// Check whether email address provided is a valid email address or not
		/// </summary>
		/// <param name="emailAddress"></param>
		/// <returns>bool</returns>
		public static bool IsValidEmailAddress(string emailAddress)
		{
			return Regex.IsMatch(emailAddress.ToLower(), RegEx.ValidEmailAddress);
		}

		/// <summary>
		/// Check if password contain disallowed characters
		/// </summary>
		/// <param name="password">The password.</param>
		/// <returns><c>true</c> if [is valid password] [the specified password]; otherwise, <c>false</c>.</returns>
		public static bool IsValidPassword(string password)
		{
			return Regex.IsMatch(password, RegEx.AllowedPasswordCharacters);
		}

		#endregion
	}
}
