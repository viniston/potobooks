using System;
using System.Collections.Generic;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Configuration
{
	public class ConfigurationController : ControllerBase<ConfigurationInfo, string>
	{
		#region Protected abstract properties

		protected override Func<DateTime?, IEnumerable<ConfigurationInfo>> GetAllFunc => DataProviderManager.Provider.GetAllConfigurations;
		protected override IEqualityComparer<string> IdentityComparer => StringComparer.InvariantCultureIgnoreCase;

		#endregion


		#region Singleton

		public static ConfigurationController Instance { get; private set; }

		#endregion


		#region Constructor

		static ConfigurationController()
		{
			Instance = new ConfigurationController();
		}

		#endregion


		#region Public Methods

		public string ValueOf(string key)
		{
			ConfigurationInfo configuration = this[key];
			if (configuration != null)
				return configuration.Value;
			else
				return null;
		}

		public T ValueOf<T>(string key) where T : IConvertible
		{
			string value = ValueOf(key);

			if (value != null)
				return (T)Convert.ChangeType(value, typeof(T));
			else
				return default(T);
		}

		#endregion
	}
}
