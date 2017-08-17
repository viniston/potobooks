using System;
using Helper;
using Photo.Business.Entities.Security;
using Resource.Master;
using Utility;

namespace Core
{
	public partial class ChangePassword : CorePage.CorePage
	{
		#region Event Handlers

		protected void Page_Load(object sender, EventArgs e)
		{
			DivMessage = divMessage;
			lblUserName.Text = User.UserName;

			// Set active Tab
			ResourceMastersAdmin master = Page.Master as ResourceMastersAdmin;
			master.TabGroup = GetGlobalResourceObject("Resource", "TabAccount").ToString();
			master.SelectedTab = GetGlobalResourceObject("Resource", "TemplateChangePasswordMenuText").ToString().ToLower().Replace(" ", string.Empty);
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
						Response.Redirect(Request.QueryString["ReturnUrl"]);
					}
					else
					{
						ShowMessage(MessageType.Information, GetLocalResourceObject("MessagePasswordChanged").ToString());
						divForm.Visible = false;
					}
				}
				else
					ShowMessage(MessageType.Error, GetLocalResourceObject("MessagePasswordChangeError").ToString());
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Validate input fields on the page
		/// </summary>
		/// <returns>bool</returns>
		private bool ValidateInputs()
		{
			if (string.IsNullOrEmpty(txtNewPassword.Text) || string.IsNullOrEmpty(txtConfirmPassword.Text) || string.IsNullOrEmpty(txtCurrentPassword.Text))
			{
				ShowMessage(MessageType.Error, GetLocalResourceObject("MessageInvalidInput").ToString());
				return false;
			}

			if (!UserController.Validate(User.UserName, txtCurrentPassword.Text))
			{
				ShowMessage(MessageType.Error, GetLocalResourceObject("MessageIncorrectCurrentPassword").ToString());
				return false;
			}

			return true;
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Business/Default.aspx");
		}

		#endregion
	}
}