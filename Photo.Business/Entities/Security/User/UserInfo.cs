using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Security;
using Photo.Business.Entities.Model;

namespace Photo.Business.Entities.Security
{
	[Serializable]
	public class UserInfo
	{
		#region Private members

		private Guid _id = Guid.Empty;
		private MembershipUser _membershipUser = null;
		private List<RoleInfo> _rolesList = null;
		private List<UserAction> _allowedActionList = null;
		private readonly DateTime _absoluteExpiry = DateTime.UtcNow.AddMinutes(Convert.ToInt16(ConfigurationManager.AppSettings["LoggedInUserDetailsExpiryInMinutes"]));

		#endregion


		#region Public properties

		public string UserName => MembershipUser?.UserName;
		public string FirstNameEN { get; set; }
		public string LastNameEN { get; set; }
        public bool IsAdmin { get; set; }
		public string NameEN => FirstNameEN + " " + LastNameEN;
		public string AdditionalDetails { get; set; }
	
		public Guid ID
		{
			get
			{
				return _membershipUser != null ? new Guid(_membershipUser.ProviderUserKey.ToString()) : _id;
			}

			set
			{
				if (_membershipUser != null && _membershipUser.ProviderUserKey.ToString() != value.ToString())
					throw new Exception("Cannot set the User's ID to a different value than the existing MembershipUser's key");

				_id = value;
			}
		}

		public MembershipUser MembershipUser
		{
			get
			{
				if (_membershipUser == null && _id != Guid.Empty)
					_membershipUser = Membership.GetUser(_id);

				return _membershipUser;
			}

			set
			{
				if (value != null && _id != Guid.Empty && value.ProviderUserKey.ToString() != _id.ToString())
					throw new Exception("Cannot set the MembershipUser while a different value for ID exists");

				_membershipUser = value;
			}
		}

        // The JavaScriptSerializer ignores this field.
        [ScriptIgnore]
        public List<RoleInfo> RolesList
		{
			get
			{
				if (ID != Guid.Empty && (_rolesList == null || IsOutdated))
					_rolesList = RoleController.Instance.GetRolesByUserID(ID);

				return _rolesList;
			}

			set
			{
				_rolesList = value;
			}
		}

        // The JavaScriptSerializer ignores this field.
        [ScriptIgnore]
        public string RoleNames
		{
			get
			{
				string roleNames = RolesList.Aggregate(string.Empty, (current, role) => current + (role.RoleName + ","));
				return roleNames?.Remove(roleNames.Length - 1);
			}
		}

        // The JavaScriptSerializer ignores this field.
        [ScriptIgnore]
        public List<UserAction> AllowedActionList
		{
			get
			{
				if (ID == Guid.Empty || (_allowedActionList != null && !IsOutdated))
					return _allowedActionList;

				_allowedActionList = new List<UserAction>();

				foreach (RoleInfo role in RolesList)
					foreach (UserActionInfo action in role.ActionsList)
						if (!_allowedActionList.Contains(action.Action))
							_allowedActionList.Add(action.Action);

				return _allowedActionList;
			}
		}

		public bool IsOutdated => _absoluteExpiry < DateTime.UtcNow;		
        
		#endregion
	}
}