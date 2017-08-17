using System;
using System.Web.Security;
using System.Web.UI;
using Helper;

namespace Photo.Presentation.Website.Core
{
	public partial class SignOut : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			SecurityHelper.RedirectToLoginPage(false);
		}
	}
}
