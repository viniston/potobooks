using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Helper;
using Photo.Business.Entities.Security;
using Utility;

namespace CorePage
{
    public class CorePage : Page
    {
        private UserInfo _user;

        protected HtmlGenericControl DivMessage;

        protected new UserInfo User
        {
            get { return _user ?? (_user = SecurityHelper.GetCurrentUser()); }
        }

        protected override void OnPreInit(EventArgs e)
        {
            //if (User == null)
            //	SecurityHelper.RedirectToLoginPage(false);
        }

        protected void ShowMessage(MessageType information, object message)
        {
            if (information == MessageType.Information)
                DivMessage.Attributes["class"] = "alert alert-info";
            else if (information == MessageType.Error)
                DivMessage.Attributes["class"] = "alert alert-error";
            else if (information == MessageType.Confirmation)
                DivMessage.Attributes["class"] = "alert alert-success";
            else if (information == MessageType.Warning)
                DivMessage.Attributes["class"] = "alert alert-warning";

            DivMessage.InnerHtml = "<a type=\"button\" class=\"close\" data-dismiss=\"alert\">×</a>" + message;
        }

        protected void ClearMessage()
        {
            DivMessage.Attributes["class"] = "hidden";
            DivMessage.InnerHtml = string.Empty;
        }

        protected void CheckPagePermission(UserAction action)
        {
            SecurityHelper.IsUserPermittedForPage(User, action);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            CrossPageMessage crossPageMessage = Utilities.GetCrossPageMessage(true);
            if (crossPageMessage != null)
                ShowMessage(crossPageMessage.MessageType, crossPageMessage.MessageText);
        }
    }
}