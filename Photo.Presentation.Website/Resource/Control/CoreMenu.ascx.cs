using System;
using Helper;
using Photo.Business.Entities.Security;

public partial class CoreMenu : System.Web.UI.UserControl
{
    private UserInfo _user;

    protected UserInfo User
    {
        get { return _user ?? (_user = SecurityHelper.GetCurrentUser()); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsAdmin)
            liSubscriptionMgmnt.Visible = false;

    }
}