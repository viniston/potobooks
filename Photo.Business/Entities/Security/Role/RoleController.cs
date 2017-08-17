using System;
using System.Collections.Generic;
using System.Web.Security;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Security
{
	public class RoleController : ControllerBase<RoleInfo, Guid>
	{
		#region protected properties

		protected override Func<DateTime?, IEnumerable<RoleInfo>> GetAllFunc => DataProviderManager.Provider.GetAllRoles;

		#endregion


		#region Singleton

		public static RoleController Instance { get; private set; }

		#endregion


		#region Constructor

		static RoleController()
		{
			Instance = new RoleController();
		}

		#endregion

		#region Public Methods

		public RoleInfo GetRoleByID(Guid roleID)
		{
			return this[roleID];
		}

		public RoleInfo GetRoleByName(string name)
		{
			return All.Find(r => String.Compare(r.RoleName, name, StringComparison.OrdinalIgnoreCase) == 0);
		}

		public List<RoleInfo> GetAllRoles()
		{
			return All;
		}

		public void AddUserToRole(UserInfo user, RoleInfo role)
		{
			if (!IsUserInRole(user, role))
				Roles.AddUserToRole(user.UserName, role.RoleName);
		}

		public void RemoveUserFromRole(UserInfo user, RoleInfo role)
		{
			if (IsUserInRole(user, role))
				Roles.RemoveUserFromRole(user.UserName, role.RoleName);
		}

		public List<RoleInfo> GetRolesByUserID(Guid userID)
		{
			//TODO: Should utilize a dictionary for caching purposes
			return new List<RoleInfo>(DataProviderManager.Provider.GetRolesByUserID(userID));
		}

		public bool IsUserInRole(UserInfo user, RoleInfo role)
		{
			//TODO: Should cache these in a dictionaty
			return Roles.IsUserInRole(user.UserName, role.RoleName);
		}

		#endregion
	}
}