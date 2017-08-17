using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Photo.Business.Utilities.Formatting;
using Photo.Resources.Shared;

namespace Helper
{
	/// <summary>
	/// Helper class to construct Email Format
	/// </summary>
	public static class UIFormatHelper
	{
		#region Public Methods

		/// <summary>
		/// Method to reformat email address.
		/// </summary>
		/// <param name="email">string</param>
		/// <returns>string</returns>
		public static string ReformatEmailAddress(string email)
		{
			MatchCollection matches = new Regex(Shared.RegexReformatEmailAddress).Matches(email);

			List<string> matchedCharacters = new List<string>();

			foreach (Match match in matches)
			{
				if (!matchedCharacters.Contains(match.Value))
				{
					email = email.Replace(match.Value, match.Value + "</span><span>");
					matchedCharacters.Add(match.Value);
				}
			}

			return "<span>" + email + "</span>";
		}

		/// <summary>
		/// Method to return only alphabets from text
		/// </summary>
		/// <param name="text">string</param>
		/// <returns>string</returns>
		public static string GetAlphabets(string text)
		{
			return FormatHelper.CleanUpNonAlphaCharactersAndKeepSpace(text);
		}

		/// <summary>
		/// Method to return only numbers from text
		/// </summary>
		/// <param name="text">string</param>
		/// <returns>string</returns>
		public static string GetNumbers(string text)
		{
			return FormatHelper.CleanUpNonNumericCharacters(text);
		}

		/// <summary>
		/// method to remove non alphanumeric characters from the text
		/// </summary>
		/// <param name="text">string</param>
		/// <returns>string</returns>
		public static string RemoveNonAlphaNumericCharacters(string text)
		{
			return FormatHelper.CleanUpNonAlphaNumericCharacters(text).ToUpper();
		}

		/// <summary>
		/// Format the date to dd/mm/yy
		/// </summary>
		/// <param name="dateToProcess"></param>
		/// <returns></returns>
		public static string FormatDate(string dateToProcess)
		{
			string dateToProcessTemp = dateToProcess.Replace(" ", "");
			string returnValue = "";
			string[] dateToProcessArray = dateToProcessTemp.Split('/');
			if (dateToProcessArray.Length == 3)
			{
				returnValue = dateToProcessArray[0] + "/" + dateToProcessArray[1] + "/" + dateToProcessArray[2];
			}
			else
			{
				returnValue = dateToProcess;
			}

			return returnValue;
		}

		public static string GetHttpMimeType(string fileExtension)
		{
			switch (fileExtension.ToLower())
			{
				case "bmp":
					return "image/bmp";

				case "csv":
					return "application/CSV";

				case "gif":
					return "image/gif";

				case "jpg":
				case "jpeg":
					return "image/jpeg";

				case "pdf":
					return "application/pdf";

				case "png":
					return "image/png";

				case "tif":
				case "tiff":
					return "image/tiff";

				default:
					return "application/octet-stream";
			}
		}

		public static void SetInvalidInputFieldClass(Literal ltlCssClass)
		{
			if (string.IsNullOrEmpty(ltlCssClass.Text))
			{
				ltlCssClass.Text = "invalid";
			}
			else
			{
				List<string> initialStyles = new List<string>(
					ltlCssClass.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
				if (!initialStyles.Contains("invalid"))
					initialStyles.Add("invalid");

				ltlCssClass.Text = string.Join(" ", initialStyles.ToArray());
			}
		}

		public static void RemoveInvalidInputFieldClass(Literal ltlCssClass)
		{
			if (!string.IsNullOrEmpty(ltlCssClass.Text))
			{
				List<string> initialStyles = new List<string>(
					ltlCssClass.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
				if (initialStyles.Contains("invalid"))
					initialStyles.Remove("invalid");

				if (initialStyles.Count != 0)
					ltlCssClass.Text = string.Join(" ", initialStyles.ToArray());
				else
					ltlCssClass.Text = string.Empty;
			}
		}

		/// <summary>
		/// Set the CSS class for input fields
		/// </summary>
		/// <param name="listItem"></param>
		/// <param name="isValid"></param>
		public static void SetCssClass(HtmlGenericControl listItem, bool isValid)
		{
			if (listItem.Attributes["class"] == null)
				listItem.Attributes["class"] = string.Empty;

			bool containsErrorCSSClass = listItem.Attributes["class"].Contains("invalid");

			if (!isValid && !containsErrorCSSClass)
				listItem.Attributes["class"] += " " + "invalid";
			else if (isValid && containsErrorCSSClass)
				listItem.Attributes["class"] = listItem.Attributes["class"].Replace("invalid", "");

			if (listItem.Attributes["class"] != null)
				listItem.Attributes["class"] = listItem.Attributes["class"].Trim();
		}

		#endregion
	}
}