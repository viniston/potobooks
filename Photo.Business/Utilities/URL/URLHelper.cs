using Photo.Business.Entities.Model;
using Photo.Business.Utilities.Formatting;
using Photo.Resources.PageLink;
using System.Configuration;
using System.Web;
//using Photo.Business.Entities.Common;
//using Photo.Business.Entities.Common.MarketProfile;

namespace Photo.Business.Utilities.URL
{
	/// <summary>
	/// Helper class for URLs
	/// </summary>
	public static class URLHelper
	{
		#region Private static members

		private static readonly string _domainName = ConfigurationManager.AppSettings["Serverdomain"];
		private static readonly string _httpPort = ConfigurationManager.AppSettings["Httpport"];
		private static readonly string _httpsPort = ConfigurationManager.AppSettings["Httpsport"];

		#endregion


		#region Private Methods

		private static string ResolveServerURL(ServerURLType urlType)
		{
			if (urlType == ServerURLType.HTTPS)
				return "http://" + _domainName + ":" + _httpPort;
			else
				return "http://" + _domainName + ":" + _httpPort;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Returns a server URL based on the provided parameters
		/// </summary>
		/// <param name="urlType"></param>
		/// <param name="marketProfile"></param>
		/// <returns>string</returns>
		public static string GetServerURL(ServerURLType urlType)
		{
			return ResolveServerURL(urlType);
		}

		/// <summary>
		/// HTML encode the URL
		/// </summary>
		/// <param name="URL"></param>
		/// <returns>string</returns>
		public static string HTMLEncode(string URL)
		{
			return HttpUtility.HtmlEncode(URL);
		}

        /// <summary>
		/// Resolve hotel details url to a friendly one
		/// </summary>
		/// <param name="ProductPropertyID"></param>
		/// <returns>string</returns>
		public static string GetResolvedProductDetailsLink(int hotelPropertyID)
        {
            ProductCategoryInfo productCategory = ProductCategoryController.Instance.GetByID(hotelPropertyID);

            if (productCategory != null)
                return PageLink.ProductDetailsPageFriendlyUrl
                        .Replace("[ProductDetailsLink]", HttpUtility.UrlEncode(FormatHelper.CleanUpNonAlphaNumericCharachtersWithDash(productCategory.Category.Name + " " + productCategory.Name)))
                        .Replace("[ProductPropertyID]", productCategory.ID.ToString());
            else
                return null;
        }

        #endregion
    }
}