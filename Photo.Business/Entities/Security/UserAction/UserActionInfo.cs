using System;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Security
{
	public class UserActionInfo : ICBO<short>
	{
		#region ICBO

		public short Identity => ID;

		public DateTime Updated { get; set; }

		public bool? IsActive
		{
			get { return null; }
			set { throw new ArgumentNullException(nameof(value)); }
		}

		#endregion


		#region Public properties

		public short ID { get; set; }

		public string NameEN { get; set; }

		public string DescriptionEN { get; set; }

		public UserAction Action => (UserAction)ID;

		#endregion
	}
}
