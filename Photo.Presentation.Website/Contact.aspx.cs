using Photo.Business.Utilities.EmailHelper;
using System;

public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["sub"]))
            contactMessage.Value = GetLocalResourceObject("SubjectFormat").ToString().Replace("[SUBJECT]", Request.QueryString["sub"]);
        
        ltlMessage.Text = GetLocalResourceObject("DefaultMessage").ToString();
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string description = "Email : " + contactEmail.Value + "<br/>" +
                             "Name : " + contactName.Value + "<br/>" +
                             "Comment : " + contactMessage.Value + "<br/>";
        EmailHelper.Contact(description);
        ltlMessage.Text = GetLocalResourceObject("SuccessSendMessage").ToString();
        contactEmail.Value = string.Empty;
        contactName.Value = string.Empty;
        contactMessage.Value = string.Empty;
    }
}