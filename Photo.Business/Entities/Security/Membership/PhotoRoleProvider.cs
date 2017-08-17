using System.Collections.Specialized;
using System.Configuration;
using System.Web.Security;

namespace Photo.Business.Entities.Security
{
	public class PhotoRoleProvider : SqlRoleProvider
	{
		#region Public override

		public override void Initialize(string name, NameValueCollection config)
		{
			config["connectionString"] = ConfigurationManager.AppSettings["PhotoConnectionString"];
			base.Initialize(name, config);
		}

		#endregion
	}
}
