using Photo.Business.Entities.Security;
using Photo.Business.Utilities.EmailHelper;
using Photo.Utility.LogHelper;
using System;

public partial class Core_Subscription : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubscribe_Click(object sender, EventArgs e)
    {
        txtFirstName.Text = txtFirstName.Text.Trim();
        txtLastName.Text = txtLastName.Text.Trim();
        txtEmail.Text = txtEmail.Text.Trim().ToLower();

        PhotoMembershipProvider membershipProvider = new PhotoMembershipProvider();
        string password = membershipProvider.GeneratePassword();

        UserInfo newUser = UserController.Create(txtFirstName.Text, txtLastName.Text, txtEmail.Text, password, txtEmail.Text, null, null);
        try
        {
            EmailHelper.WelcomeUserEmail(newUser, password);
        }
        catch(Exception ex)
        {
            LogHelper.Log(Logger.Application, LogLevel.Error, "Failed to sent email to the user. \n" + ex.Message);
            return;
        }
        if (newUser == null)
            throw new Exception("Could not create user");

        Response.Redirect("Default.aspx");
    }
}