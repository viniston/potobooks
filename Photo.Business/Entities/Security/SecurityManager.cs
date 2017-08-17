using System.Collections;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Photo.Business.Entities.Security
{
	/// <summary>
	/// A manager class to perform security related checks
	/// </summary>
	public static class SecurityManager
	{
		#region Private Members

		private static string _md5SignatureKey = ConfigurationManager.AppSettings["MD5SignatureKey"];

		#endregion

		
		#region Public Methods

		#region User permissions

		/// <summary>
		/// Method to check if a particular user role is permitted for a particular action
		/// </summary>
		/// <param name="user">UserInfo</param>
		/// <param name="action">Action</param>
		/// <returns>bool</returns>
		public static bool IsUserPermittedForAction(UserInfo user, UserAction action)
		{
			return (user != null && user.AllowedActionList.Contains(action));
		}

		#endregion


		#region Hashing and Object Security key generation + validation

		/// <summary>
		/// Calcualtes the MD5 of the provided raw data
		/// </summary>
		/// <param name="rawData"></param>
		/// <param name="enforceUpperCaseMode">True converts the entire output to Upper case, else it will convert it to lower case</param>
		/// <returns>string</returns>
		public static string GetMD5Hash(string rawData, bool enforceUpperCaseMode)
		{
			if (string.IsNullOrEmpty(rawData))
				return string.Empty;

			MD5 hasher = MD5CryptoServiceProvider.Create();
			byte[] hashValue = hasher.ComputeHash(Encoding.ASCII.GetBytes(rawData));

			string strHex = string.Empty;
			foreach (byte b in hashValue)
				strHex += b.ToString("x2");

			return enforceUpperCaseMode ? strHex.ToUpper() : strHex.ToLower();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="objectData">SortedList</param>
		/// <param name="key">string</param>
		/// <returns>string</returns>
		public static string CalculateHash(SortedList objectData, string key)
		{
			string rawHashData = key;
			foreach (DictionaryEntry item in objectData)
			{
				string _value = item.Value == null ? string.Empty : item.Value.ToString();
				rawHashData += (_value);
			}

			return GetMD5Hash(rawHashData, true);
		}

		#endregion

		#endregion


		#region Static Constructor

		static SecurityManager()
		{
	
		}

		#endregion
	}
}