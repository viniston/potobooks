using System.Configuration;

namespace Photo.Business.DataProvider
{
	/// <summary>
	/// DataProviderConfiguration responsible for configuration related settings
	/// </summary>
	public class DataProviderConfiguration : ConfigurationSection
	{
		/// <summary>
		/// Public readonly configuration property for setting providers
		/// </summary>
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base["providers"];

		/// <summary>
		/// Public configuration property for default provider name
		/// </summary>
		[ConfigurationProperty("default", DefaultValue = "SqlProvider")]
		public string Default
		{
			get
			{
				return (string)base["default"];
			}
			set
			{
				base["default"] = value;
			}
		}
	}
}
