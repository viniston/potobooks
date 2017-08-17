using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Photo.Business.Entities.Security;
using Photo.Business.Utilities.URL;
using Photo.Resources.PageLink;

namespace Helper {
    /// <summary>
    /// Helper class for security-related operations
    /// </summary>
    public static class SecurityHelper {
        #region Private Members

        private const string _userSessionKeyName = "_LoggedInUser_";
        private static readonly Dictionary<long, bool> _ipLookupResults;

        #endregion


        #region Constructor

        static SecurityHelper() {
            _ipLookupResults = new Dictionary<long, bool>();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to add currently logged in user to session
        /// </summary>
        /// <param name="user"></param>
        private static void StoreUserInSession(UserInfo user) {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
                HttpContext.Current.Session[_userSessionKeyName] = user;
        }

        /// <summary>
        /// Get currently logged in user object from session
        /// </summary>
        /// <returns></returns>
        private static UserInfo RetrieveUserFromSession() {
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[_userSessionKeyName] != null)
                return (UserInfo)HttpContext.Current.Session[_userSessionKeyName];
            else
                return null;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Method to signout currently logged in user and destroys user session
        /// </summary>
        /// <param name="redirectToLoginPage">bool</param>
        public static void SignOut(bool redirectToLoginPage) {
            FormsAuthentication.SignOut();

            if (HttpContext.Current != null) {
                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session[_userSessionKeyName] = null;

                HttpContext.Current.Response.Redirect(redirectToLoginPage
                    ? URLHelper.GetServerURL(ServerURLType.HTTP) + PageLink.SignInPage
                    : HttpContext.Current.Request.RawUrl);
            }
        }

        /// <summary>
        /// Method to redirect to login page
        /// </summary>
        public static void RedirectToLoginPage(bool redirectWithReturnURL) {
            HttpContext.Current.Response.Redirect(URLHelper.GetServerURL(ServerURLType.HTTP)
                + (redirectWithReturnURL
                    ? PageLink.SignInPageWithReturnUrl
                        .Replace("[ReturnUrl]", HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.RawUrl))
                    : PageLink.SignInPage));
        }
        
        /// <summary>
        /// Method to clear user session if exist
        /// </summary>
        public static void ClearUserSession() {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
                HttpContext.Current.Session[_userSessionKeyName] = null;
        }

        /// <summary>
        /// Method to get currently logged in user
        /// </summary>
        /// <returns>UserInfo</returns>
        public static UserInfo GetCurrentUser() {
            UserInfo user = RetrieveUserFromSession();
            if (user == null || user.IsOutdated) {
                if (HttpContext.Current != null && HttpContext.Current.Request.IsAuthenticated)
                    user = UserController.GetCurrentUser();

                if (user != null && user.MembershipUser != null) {
                    if (!user.MembershipUser.IsApproved || user.MembershipUser.IsLockedOut)
                        SignOut(false);
                    else
                        StoreUserInSession(user);
                }
            }

            return user;
        }

        /// <summary>
        /// Method to check if any role of user is permitted to access the page
        /// </summary>
        /// <param name="user">UserInfo</param>
        /// <param name="action">Action</param>
        public static void IsUserPermittedForPage(UserInfo user, UserAction action) {
            if (!SecurityManager.IsUserPermittedForAction(user, action))
                HttpContext.Current.Response.Redirect(PageLink.UnauthorizedPage);
        }

        /// <summary>
        /// Verify the URL protocol 
        /// </summary>
        /// <returns>bool</returns>
        public static bool VerifyForHTTPS() {
            return (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables["HTTPS"].ToUpper() == "ON");
        }

        /// <summary>
        /// Checks if the user needs to be notified to update their password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool</returns>
        public static bool CheckForPasswordChangeNotification(UserInfo user) {
            return false;
        }

        /// <summary>
        /// Check if protocol is HTTP then redirect it to HTTPS
        /// </summary>
        public static void CheckAndRedirectToHTTPS() {
            if (!HttpContext.Current.Request.IsSecureConnection)
                HttpContext.Current.Response.Redirect(URLHelper.GetServerURL(ServerURLType.HTTP)
                    + HttpContext.Current.Request.RawUrl, true);
        }


        #endregion
    }
}