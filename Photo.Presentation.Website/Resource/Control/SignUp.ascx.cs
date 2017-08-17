using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Helper;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Security;
using Photo.Business.Utilities.Formatting;
using Photo.Resources.PageLink;
using Photo.Resources.Shared;
using Photo.Utility.Validation;
using Utility;

namespace Resource.Control {
    public partial class SignUp : UserControl {
        #region Private Members

        private UserInfo _user;

        #endregion


        #region Public Properties

        public UserInfo User {
            get { return _user ?? (_user = SecurityHelper.GetCurrentUser()); }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// method to validate login inputs
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateInputs() {

            var isValid =
                !(string.IsNullOrEmpty(txtFirstName.Value) ||
                  string.IsNullOrEmpty(txtLastName.Value) || string.IsNullOrEmpty(txtUserName.Value) ||
                  !ValidationHelper.IsValidEmailAddress(
                      txtUserName.Value.ToLower()));

            if (string.IsNullOrEmpty(txtCKPassword.Value))
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Display the proper error message to the user
        /// </summary>
        /// <param name="messageName"></param>
        private void ShowValidationMessage(string messageName) {
            divMessage.Visible = true;
            var localResourceObject = GetLocalResourceObject(messageName);
            if (localResourceObject != null)
                ltlMessage.Text = localResourceObject.ToString();
        }

        #endregion


        #region Events Handlers

        protected void Page_Load(object sender, EventArgs e) {
            if (IsPostBack) return;
        }

        /// <summary>
        /// Sign In Button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSignUp_Click(object sender, EventArgs e) {

            txtUserName.Value = txtUserName.Value.Trim().ToLower();
            txtCKPassword.Value = FormatHelper.CleanUpInvalidPasswordCharacters(txtCKPassword.Value);

            if (!ValidateInputs()) {
                ShowValidationMessage("MessageRequiredField");
                return;
            }


            if (!txtCKPassword.Value.Equals(txtCKConfirmPassword.Value)) {
                ShowValidationMessage("ConfirmPasswordMisMatch");
                return;
            }

            if (UserController.GetByUserName(txtUserName.Value) != null) {
                ShowValidationMessage("UserNameAlreadyInUse");
                return;
            }

            #region user reation

            var transaction = DataProviderManager.Provider.NewDataTransaction;
            var roleList = new List<RoleInfo>();
            var newUserInfo = UserController.Create(txtFirstName.Value, txtLastName.Value, txtUserName.Value,
                txtCKPassword.Value,
                txtUserName.Value, roleList, transaction);
            transaction.Commit();

            if (newUserInfo != null)
                FormsAuthentication.RedirectToLoginPage();

            #endregion
        }

        #endregion
    }
}