using System.Configuration.Provider;

namespace Photo.Business.DataProvider
{
	/// <summary>
	/// DataProviderCollection class to get all data providers into providercollection 
	/// </summary>
	public class DataProviderCollection : ProviderCollection
	{
		/// <summary>
		/// DataProviderCollection constructor
		/// </summary>
		/// <param name="name">Name of the provider to be returned</param>
		public new DataProvider this[string name] => (DataProvider)base[name];
	}
}
