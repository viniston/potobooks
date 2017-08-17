using System;
using System.Linq;
using System.Web.UI.WebControls;
using Photo.Business.Entities.Model;
using Photo.Business.Entities.Security;
using Helper;

public partial class _Default : System.Web.UI.Page
{
    private UserInfo _user;
    protected new UserInfo User
    {
        get { return _user ?? (_user = SecurityHelper.GetCurrentUser()); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (User != null)
            Response.Redirect("/Business/Default.aspx");

		if(!IsPostBack)
		{
			
		}
    }

	
}