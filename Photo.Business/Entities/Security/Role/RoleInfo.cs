using System;
using System.Collections.Generic;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Security
{
	public class RoleInfo : ICBO<Guid>
	{
		#region Private members

		string _actionDetails = null;
		List<UserActionInfo> _actionList = new List<UserActionInfo>();

		#endregion

		#region Public properties

		public Guid ID { get; set; }

		public Guid ApplicationID { get; set; }

		public string RoleName { get; set; }

		public byte RoleTypeID { get; set; }

		public string Description { get; set; }

		public string ActionDetails
		{
			get
			{
				//If no '_actionList' is built already then we simply return '_actionDetails'
				//Otherwise, we use '_actionList' to build a new CSV string
				lock (_actionDetails)
				{
					if (_actionList == null || _actionList.Count <= 0)
						return _actionDetails;

					string[] userActionIDs = new string[_actionList.Count];
					for (int i = 0; i < _actionList.Count; i++)
					{
						userActionIDs[i] = _actionList[i].ID.ToString();
					}

					_actionDetails = string.Join(",", userActionIDs);
				}

				return _actionDetails;
			}

			set
			{
				_actionDetails = value;
				_actionList = null;
			}
		}

		public List<UserActionInfo> ActionsList
		{
			get
			{
				if (_actionList == null)
				{
					if (!string.IsNullOrEmpty(ActionDetails))
					{
						//Just in case more than one thread is trying to access the same RoleInfo object
						lock (_actionDetails)
						{
							string[] userActionIDs = ActionDetails.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
							_actionList = new List<UserActionInfo>();
							foreach (string userActionID in userActionIDs)
							{
								_actionList.Add(UserActionController.Instance.GetByID(short.Parse(userActionID)));
							}
						}
					}
				}

				return _actionList;
			}
		}

		#endregion

		#region ICBO

		public Guid Identity => ID;

		public DateTime Updated { get; set; }

		public bool? IsActive
		{
			get { return null; }
			set { throw new ArgumentNullException(nameof(value)); }
		}

		#endregion
	}
}