using System;
using System.Configuration;
using System.Web.Configuration;

namespace Photo.Business.DataProvider
{
	/// <summary>
	/// DataProviderManager class used to manage all data related methods
	/// </summary>
	public class DataProviderManager
	{
		private static DataProvider _defaultProvider;
		private static DataProviderCollection _providers;

		/// <summary>
		/// Static contructor for DataProviderManager
		/// </summary>
		static DataProviderManager()
		{
			Initialize();
		}

		/// <summary>
		/// Private static method used to initialize the basic settings
		/// </summary>
		/// <returns>void</returns>
		private static void Initialize()
		{			
			DataProviderConfiguration configuration = (DataProviderConfiguration) ConfigurationManager.GetSection("PhotoDataProvider");

			if (configuration == null)
				throw new ConfigurationErrorsException ("PhotoDataProvider configuration section is not set correctly.");

			_providers = new DataProviderCollection();

			ProvidersHelper.InstantiateProviders(configuration.Providers, _providers, typeof(DataProvider));

			_providers.SetReadOnly();

			_defaultProvider = _providers[configuration.Default];

			if (_defaultProvider == null)
				throw new Exception("defaultProvider");			
		}

		/// <summary>
		/// Public static readonly property used to get DataProvider object
		/// </summary>
		public static DataProvider Provider => _defaultProvider;

		/// <summary>
		/// Public static readonly property used to get DataProviderCollection object
		/// </summary>
		public static DataProviderCollection Providers => _providers;
	}
}
