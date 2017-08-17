using System;
using System.Web.UI;
using Photo.Business.Entities.Security;

namespace Resource.Master {
    public partial class ResourceMastersAdmin : MasterPage {
        public ResourceMastersAdmin(UserInfo user) {
        }

        public ResourceMastersAdmin() {
        }

        // ReSharper disable once ValueParameterNotUsed
        public string TabGroup {
            set { }
        }

        // ReSharper disable once ValueParameterNotUsed
        public string SelectedTab {
            set { }
        }

        // ReSharper disable once ValueParameterNotUsed
        public string MainHeading {
            set { }
        }

        protected void Page_Load(object sender, EventArgs e) {
        }
    }
}
