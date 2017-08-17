using System;
using System.Web.Security;
using System.Web.UI;
using Helper;
using Photo.Business.Entities.Security;
using Photo.Business.Utilities.Formatting;
using Photo.Resources.PageLink;
using Photo.Resources.Shared;
using Photo.Utility.Validation;
using Utility;

namespace Resource.Control
{
    public partial class SignIn : UserControl
    {
        #region Private Members

        private UserInfo _user;

        #endregion


        #region Public Properties

        public UserInfo User
        {
            get { return _user ?? (_user = SecurityHelper.GetCurrentUser()); }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// method to validate login inputs
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateInputs()
        {
            var isValid =
                !(string.IsNullOrEmpty(txtEmailAddress.Value) ||
                  !ValidationHelper.IsValidEmailAddress(txtEmailAddress.Value.ToLower()));

            if (string.IsNullOrEmpty(txtCKPassword.Value))
                isValid = false;

            return isValid;
        }

        #endregion


        #region Events Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (User != null)
                FormsAuthentication.RedirectFromLoginPage(User.UserName, true);

            CookieHelper.SaveCheckCookie(Request, Response);
        }

        /// <summary>
        /// Sign In Button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            if (!CookieHelper.CheckIfCookiesSupported(Request))
            {
                Response.Redirect(PageLink.CookiesNotAllowedPage);
                return;
            }

            txtEmailAddress.Value = txtEmailAddress.Value.Trim().ToLower();
            txtCKPassword.Value = FormatHelper.CleanUpInvalidPasswordCharacters(txtCKPassword.Value);

            if (!ValidateInputs())
            {
                divMessage.Visible = true;
                var localResourceObject = GetLocalResourceObject("MessageRequiredField");
                if (localResourceObject != null)
                    ltlMessage.Text = localResourceObject.ToString();
                return;
            }

            if (UserController.Validate(txtEmailAddress.Value, txtCKPassword.Value))
            {
                if (
                    SecurityHelper.CheckForPasswordChangeNotification(UserController.GetByUserName(txtEmailAddress.Value)))
                {
                    FormsAuthentication.SetAuthCookie(txtEmailAddress.Value, chkRememberMe.Checked);
                    Utilities.SetCrossPageMessage(Shared.ChangePasswordNotificationMessage, MessageType.Information);
                    Response.Redirect(PageLink.ChangePasswordPageWithReturnURL.Replace("[ReturnURL]",
                        PageLink.DefaultPage));
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(txtEmailAddress.Value, chkRememberMe.Checked);
                }
            }
            else
            {
                divMessage.Visible = true;
                var localResourceObject = GetLocalResourceObject("MessageLoginError");
                if (localResourceObject != null)
                    ltlMessage.Text = localResourceObject.ToString();
            }
        }

        #endregion
    }
}