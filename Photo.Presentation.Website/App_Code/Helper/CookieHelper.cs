using System;
using System.Configuration;
using System.Web;

namespace Helper
{
	public static class CookieHelper
	{
		#region Pirvate Members

		private static string _marketPreferenceCookieDomain = ConfigurationManager.AppSettings["MarketPreferenceCookieDomain"];
		private static int _marketPreferenceCookieExpiresInDays = Convert.ToInt32(ConfigurationManager.AppSettings["MarketPreferenceCookieExpiresInDays"]);
		private static string _pageTrackingCookieDomain = ConfigurationManager.AppSettings["PageTrackingCookieDomain"];
		private static int _pageTrackingCookieExpiresInDays = Convert.ToInt32(ConfigurationManager.AppSettings["PageTrackingCookieExpiresInDays"]);

		#endregion


		#region Public Methods

		public static void SaveCheckCookie(HttpRequest request, HttpResponse response)
		{
			if (request.Cookies["CheckCookie"] == null)
			{
				HttpCookie saveCookie = new HttpCookie("CheckCookie");
				saveCookie.Expires = DateTime.UtcNow.AddDays(365);
				saveCookie.Value = "0";
				response.Cookies.Add(saveCookie);
			}
		}

		public static bool CheckIfCookiesSupported(HttpRequest request)
		{
			return (request.Cookies["CheckCookie"] != null);
		}

		public static void SaveMarketPreferenceCookie(HttpResponse response)
		{
			HttpCookie cookie = new HttpCookie("MarketPreference");
			cookie.Domain = _marketPreferenceCookieDomain;
			cookie.Expires = DateTime.UtcNow.AddDays(_marketPreferenceCookieExpiresInDays);
			cookie.Value = "1";
			response.Cookies.Add(cookie);
		}

		public static void SavePageTrackingCookie(HttpRequest request, HttpResponse response, string value)
		{
			if (request.Cookies["Ad"] != null && request.Cookies["Ad"]["last-referrer"] != null
				&& request.Cookies["Ad"]["last-referrer"] == value && response!=null)
			{
				HttpCookie httpCookie = response.Cookies["Ad"];
				if (httpCookie != null)
					httpCookie.Expires = DateTime.UtcNow.AddDays(_pageTrackingCookieExpiresInDays);

				HttpCookie cookie = response.Cookies["Ad"];
				if (cookie != null) cookie["last-referrer"] = value;
			}
			else
			{
				HttpCookie cookie = new HttpCookie("Ad");
				cookie.Domain = _pageTrackingCookieDomain;
				cookie.Expires = DateTime.UtcNow.AddDays(_pageTrackingCookieExpiresInDays);
				cookie.Values.Add("last-referrer", value);
				if (response != null)
					response.Cookies.Add(cookie);
			}
		}

		public static string GetPageTrackingCookie(HttpRequest request)
		{
			if (request.Cookies["Ad"] != null && request.Cookies["Ad"]["last-referrer"] != null)
				return request.Cookies["Ad"]["last-referrer"];

			return string.Empty;
		}

		public static void SaveInsuranceCheckCookie(HttpResponse response, string value)
		{
			HttpCookie cookie = new HttpCookie("InsuranceCheck");
			cookie.Value = value;
			cookie.Expires = DateTime.UtcNow.AddDays(365);
			response.Cookies.Add(cookie);
		}

		public static string GetInsuranceCheckCookie(HttpRequest request)
		{
			if (request.Cookies["InsuranceCheck"] != null)
				return request.Cookies["InsuranceCheck"].Value;

			return string.Empty;
		}

		#endregion
	}
}