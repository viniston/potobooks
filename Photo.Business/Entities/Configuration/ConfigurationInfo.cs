using System;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Configuration
{
	public class ConfigurationInfo : ICBO<string>
	{
		#region ICBO

		public string Identity => Name.ToLower();

		public DateTime Updated { get; set; }

		public bool? IsActive
		{
			get { return null; }
			set { throw new ArgumentNullException(nameof(value)); }
		}

		#endregion


		#region Public properties

		/// <summary>
		/// Configuration value name. Also used as Identity
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Configuration value represented as a string
		/// </summary>
		public string Value { get; set; }

		#endregion
	}
}
