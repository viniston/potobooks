using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using Helper;
using Photo.Business.Entities.Security;
using Photo.Business.Utilities.EmailHelper;
using Photo.Resources.PageLink;
using Resource.Master;
using Utility;

namespace User {
    public partial class Core_User_AddEdit : CorePage.CorePage
    {
        private Target _targetEnvironment = (Target)Enum.Parse(typeof(Target), ConfigurationManager.AppSettings["Target"].ToString());

        #region Private Members

        private List<RoleInfo> _allowedRoles;
        private UserInfo _userToEdit;
        #endregion


        #region Private Properties

        protected UserInfo UserToEdit
        {
            get
            {
                if (_userToEdit == null)
                {
                    string userNameQS = Request.QueryString["UserName"];
                    if (!string.IsNullOrEmpty(userNameQS))
                    {
                        _userToEdit = UserController.GetByUserName(userNameQS);

                        if (_userToEdit == null)
                            Utilities.RedirectToErrorPage();

                        return _userToEdit;
                    }
                    return null;
                }
                return _userToEdit;
            }
        }

        protected string UserName
        {
            get { return UserToEdit != null ? UserToEdit.UserName : string.Empty; }
        }

        private List<RoleInfo> AllowedRoles
        {
            get
            {
                if (_allowedRoles == null)
                {
                    _allowedRoles = RoleController.Instance.GetAllRoles();
                }

                return _allowedRoles;
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            CheckPagePermission(UserAction.UserManage);

            if (User != null && UserToEdit != null)
            {
                Response.Redirect(PageLink.UnauthorizedPage);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (User == null)
                SecurityHelper.RedirectToLoginPage(false);

            ResourceMastersAdmin master = Page.Master as ResourceMastersAdmin;


            if (!IsPostBack)
            {
                chkRoleList.DataSource = AllowedRoles;
                chkRoleList.DataTextField = "RoleName";
                chkRoleList.DataValueField = "RoleName";
                chkRoleList.DataBind();

                if (UserToEdit != null)
                    PopulateUser();
            }

            master.TabGroup = GetGlobalResourceObject("Resource", "TabAccount").ToString();
            master.SelectedTab = GetGlobalResourceObject("Resource", "TemplateUserMenuText").ToString().ToLower().Replace(" ", string.Empty);

            ltlBackLink.Text = GetLocalResourceObject("ltlBackLink.Text").ToString();

        }

        private void PopulateUser()
        {
            txtFirstNameEN.Text = UserToEdit.FirstNameEN.Trim();
            txtLastNameEN.Text = UserToEdit.LastNameEN.Trim();
            txtEmail.Text = UserToEdit.UserName.Trim();
            txtEmail.ReadOnly = true;

            foreach (RoleInfo role in UserToEdit.RolesList)
            {
                ListItem item = chkRoleList.Items.FindByValue(role.RoleName);
                if (item != null)
                    item.Selected = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            txtFirstNameEN.Text = txtFirstNameEN.Text.Trim();
            txtLastNameEN.Text = txtLastNameEN.Text.Trim();
            txtEmail.Text = txtEmail.Text.Trim().ToLower();

            List<RoleInfo> roles = new List<RoleInfo>();

            if (UserToEdit != null)
            {
                foreach (RoleInfo notselectedRole in RoleController.Instance.All)
                    RoleController.Instance.RemoveUserFromRole(UserToEdit, notselectedRole);
            }

            foreach (ListItem item in chkRoleList.Items)
            {
                if (item.Selected)
                {
                    RoleInfo role = RoleController.Instance.GetRoleByName(item.Value);

                    if (role != null)
                        roles.Add(role);
                }
            }

            if (UserToEdit != null)
            {
                foreach (RoleInfo selectedRole in roles)
                    RoleController.Instance.AddUserToRole(UserToEdit, selectedRole);

                UserToEdit.FirstNameEN = txtFirstNameEN.Text;
                UserToEdit.LastNameEN = txtLastNameEN.Text;
                
                UserController.Update(UserToEdit);
            }
            else
            {
                PhotoMembershipProvider membershipProvider = new PhotoMembershipProvider();
                string password = membershipProvider.GeneratePassword();

                UserInfo newUser = UserController.Create(txtFirstNameEN.Text, txtLastNameEN.Text,
                    txtEmail.Text, password, txtEmail.Text, roles, null);

                if (_targetEnvironment != Target.Development)
                    EmailHelper.WelcomeUserEmail(newUser, password);

                if (newUser == null)
                    throw new Exception("Could not create user");
            }

            Utility.Utilities.SetCrossPageMessage("Please check your mail", MessageType.Confirmation);
            Response.Redirect("~/Business/Default.aspx");
        }
    }
}