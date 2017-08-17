using System;
using System.Collections.Generic;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Security
{
	public class UserActionController : ControllerBase<UserActionInfo, short>
	{
		#region protected properties

		protected override Func<DateTime?, IEnumerable<UserActionInfo>> GetAllFunc
		{
			get
			{
				return DataProviderManager.Provider.GetAllUserActions;
			}
		}

		#endregion


		#region Singleton

		public static UserActionController Instance { get; private set; }

		#endregion


		#region Constructor

		static UserActionController()
		{
			Instance = new UserActionController();
		}

		#endregion


		#region Public Methods

		public UserActionInfo GetByID(short id)
		{
			return All.Find(action => action.ID == id);
		}

		public UserActionInfo GetByUserAction(UserAction userAction)
		{
			return All.Find(action => action.ID == (short)userAction);
		}

		#endregion
	}
}
