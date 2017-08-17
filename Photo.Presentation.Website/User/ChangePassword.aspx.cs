using System;
using System.Web.Security;
using System.Web.UI;
using Helper;
using Photo.Business.Entities.Security;
using Photo.Resources.Shared;
using Photo.Utility.Validation;
using Utility;

namespace Core.User
{
	public partial class ChangePassword : Page
	{
		#region Private Members

		/// <summary>
		/// Holds currently logged in user
		/// </summary>
		private UserInfo _user = null;

		#endregion


		#region Event Handlers

		protected void Page_Load(object sender, EventArgs e)
		{
			if (User == null)
				SecurityHelper.RedirectToLoginPage(false);

			//check if the user is permitted to access the page
			SecurityHelper.IsUserPermittedForPage(User, UserAction.ChangeMyPassword);

			Title = Shared.SiteTitlePrefix + (string)GetLocalResourceObject("litHeading.Text") + Shared.SiteTitleSuffix;

			//Resource_MasterPage_PlusMasterPage master = (Resource_MasterPage_PlusMasterPage)Page.Master;
			//master.PanoramaHeading = Shared.SiteTitlePrefix + (string)GetLocalResourceObject("litHeading.Text");
			//master.InitSubNavigation(PlusSubNavigationItem.ChangePassword);
			//master.ContentClass = GetLocalResourceObject("ContentDivClass").ToString();
			//Control ctrlHeader = master.FindControl("ctrlHeader");
			//if (ctrlHeader is PageHeaderBase)
			//	((PageHeaderBase)ctrlHeader).SelectTab(WebsiteTab.Plus);


			if (!IsPostBack)
			{
				CrossPageMessage crossPageMessage = Utilities.GetCrossPageMessage(true);
				if (crossPageMessage != null)
					ShowMessage(crossPageMessage.MessageText, crossPageMessage.MessageType);

				if (User != null)
					litUser.Text = User.UserName;
			}
		}

		protected void btnChangePassword_Click(object sender, EventArgs e)
		{
			if (ValidateInputs())
			{
				if (UserController.ChangePassword(User, txtCurrentPassword.Text, txtNewPassword.Text))
				{
					SecurityHelper.ClearUserSession();

					if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
					{
						Utilities.SetCrossPageMessage(GetLocalResourceObject("MessagePasswordChanged").ToString(), MessageType.Confirmation);
						Response.Redirect(Request.QueryString["ReturnUrl"]);
					}
					else
					{
						ShowMessage(GetLocalResourceObject("MessagePasswordChanged").ToString(), MessageType.Confirmation);
						ulChangePassword.Visible = false;
					}
				}
				else
					ShowMessage((string)GetLocalResourceObject("MessagePasswordChangeError"), MessageType.Error);
			}
		}

		#endregion


		#region Public Properties

		/// <summary>
		/// A property to get/set the currently logged in user
		/// </summary>
		public new UserInfo User
		{
			get
			{
				if (_user == null)
					_user = SecurityHelper.GetCurrentUser();

				return _user;
			}
		}

		#endregion


		#region Private Methods

		private void ShowMessage(string messageText, MessageType messageType)
		{
			Utilities.ShowMessage(ltlMessage, messageText, messageType);
		}

		/// <summary>
		/// Validate input fields on the page
		/// </summary>
		/// <returns>bool</returns>
		private bool ValidateInputs()
		{
			if (string.IsNullOrEmpty(txtNewPassword.Text) || string.IsNullOrEmpty(txtConfirmPassword.Text) || string.IsNullOrEmpty(txtCurrentPassword.Text))
			{
				ShowMessage(GetLocalResourceObject("MessageInvalidInput").ToString(), MessageType.Warning);

				if (string.IsNullOrEmpty(txtCurrentPassword.Text))
				{
					litCurrentPasswordError.Text = GetLocalResourceObject("MessageCurrentPasswordRequired").ToString();
				}

				if (string.IsNullOrEmpty(txtNewPassword.Text))
				{
					litNewPasswordError.Text = GetLocalResourceObject("MessageNewPasswordRequired").ToString();
				}

				if (string.IsNullOrEmpty(txtConfirmPassword.Text))
				{
					litConfirmPasswordError.Text = GetLocalResourceObject("MessageConfirmPasswordRequired").ToString();
				}

				return false;
			}

			if (!UserController.Validate(User.UserName, txtCurrentPassword.Text))
			{
				ShowMessage(GetLocalResourceObject("MessageInvalidInput").ToString(), MessageType.Warning);
				litCurrentPasswordError.Text = GetLocalResourceObject("MessageIncorrectCurrentPassword").ToString();
				return false;
			}

			if (!ValidationHelper.IsValidPassword(txtNewPassword.Text))
			{
				//ShowMessage(GetLocalResourceObject("MessagePasswordContainsInvalidCharacters").ToString().Replace("[PasswordGuide]", PageLink.PasswordGuidelinesPage), MessageType.Warning);
				//UIFormatHelper.SetInvalidInputFieldClass(litNewPasswordClass);
				//litNewPasswordError.Text = GetLocalResourceObject("MessageWeakPassword").ToString().Replace("[PasswordGuide]", PageLink.PasswordGuidelinesPage);
				return false;
			}

			if (!string.IsNullOrEmpty(txtNewPassword.Text) && txtNewPassword.Text.Length < Membership.MinRequiredPasswordLength)
			{
				//ShowMessage(GetLocalResourceObject("MessageInvalidInput").ToString(), MessageType.Warning);
				//UIFormatHelper.SetInvalidInputFieldClass(litNewPasswordClass);
				//litNewPasswordError.Text = GetLocalResourceObject("MessagePasswordContainsInvalidCharacters").ToString().Replace("[PasswordGuide]", PageLink.PasswordGuidelinesPage);
				return false;
			}

			if (txtNewPassword.Text != txtConfirmPassword.Text)
			{
				ShowMessage(GetLocalResourceObject("MessageInvalidInput").ToString(), MessageType.Warning);
				litConfirmPasswordError.Text = GetLocalResourceObject("MessageNewAndConfirmPasswordMismatch").ToString();
				return false;
			}

			return true;
		}

		#endregion
	}
}