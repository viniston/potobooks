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
    public partial class AlbumHeader : UserControl {
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
        /// Set logged in user name
        /// </summary>
        private void SetUserName() {
            if (!Request.IsAuthenticated) return;
            userName.Text = User.FirstNameEN + User.LastNameEN;
        }

        #endregion



        #region Events Handlers

        protected void Page_Load(object sender, EventArgs e) {
            if (IsPostBack) return;
            if (User != null)
                SetUserName();
            else
                userName.Visible = false;
        }

        /// <summary>
        /// Sign Out Button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSignOut_Click(object sender, EventArgs e) {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
        }

        #endregion
    }
}