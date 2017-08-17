using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Helper;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Album;
using Photo.Business.Entities.Model;
using Photo.Business.Entities.Security;

namespace Business {
    public partial class BusinessDefault : System.Web.UI.Page {

        #region Private Members

        private UserInfo _user;

        #endregion

        #region Public Properties

        public UserInfo User {
            get { return _user ?? (_user = SecurityHelper.GetCurrentUser()); }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            apiUrl.Value = ConfigurationManager.AppSettings["AppUrl"];

        }
    }
}