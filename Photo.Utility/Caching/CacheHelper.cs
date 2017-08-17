using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Photo.Utility.Caching
{
	public static class CacheHelper<T> where T : class
	{
		#region Private Members

		private static MemoryCache cache = MemoryCache.Default;

		private static CacheItemPolicy DefaultCacheItemPolicy = new CacheItemPolicy()
		{
			SlidingExpiration = new TimeSpan(0, 15, 0)//The expiry time should be set from config
		};

		#endregion


		#region Public Methods

		/// <summary>
		/// Get the ItemList from cache
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <param name="itemList"></param>
		private static void Set(string cacheKey, IEnumerable<T> itemList)
		{
			cache.Set(cacheKey, itemList, DefaultCacheItemPolicy);
		}

		/// <summary>
		/// Get the given item from the cache
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <param name="cacheItem"></param>
		/// <param name="policy"></param>
		public static void Set(string cacheKey, T cacheItem, CacheItemPolicy policy = null)
		{
			policy = policy ?? DefaultCacheItemPolicy;
			if (true /* IsCachingEnabled */ )
			{
				cache.Set(cacheKey, cacheItem, policy);
			}
		}

		/// <summary>
		/// Insert the cache from database using the method deligate
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <param name="getData"></param>
		/// <param name="policy"></param>
		public static void Set(string cacheKey, Func<T> getData, CacheItemPolicy policy = null)
		{
			Set(cacheKey, getData(), policy);
		}

		/// <summary>
		/// Try to get item from cache, if not found, get it from database and store it in the cache and return
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <param name="getData"></param>
		/// <param name="returnData"></param>
		/// <param name="policy"></param>
		/// <returns></returns>
		public static bool TryGetAndSet(string cacheKey, Func<T> getData, out T returnData, CacheItemPolicy policy = null)
		{
			policy = policy ?? DefaultCacheItemPolicy;

			if (TryGet(cacheKey, out returnData))
			{
				return true;
			}

			returnData = getData();
			Set(cacheKey, returnData, policy);
			return returnData != null;
		}

		/// <summary>
		/// Get the item from the cache
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <param name="returnItem"></param>
		/// <returns></returns>
		public static bool TryGet(string cacheKey, out T returnItem)
		{
			returnItem = (T)cache[cacheKey];
			return returnItem != null;
		}

		/// <summary>
		/// Insert an item to the existing cache
		/// </summary>
		/// <param name="item"></param>
		/// <param name="cacheKey"></param>
		public static void InsertItem(T item, string cacheKey)
		{
			List<T> itemList = (List<T>)cache[cacheKey];

			if (itemList != null && itemList.Count != 0)
			{
				itemList.Add(item);
				Set(cacheKey, itemList);
			}
		}
		
		/// <summary>
		/// Update an item in the existing cache
		/// </summary>
		/// <param name="item"></param>
		/// <param name="cacheKey"></param>
		public static void UpdateItem(T item, string cacheKey)
		{
			List<T> itemList = (List<T>)cache[cacheKey];

			if (itemList != null && itemList.Count != 0)
			{
				if (itemList.Contains(item))
					itemList.Remove(item);

				itemList.Add(item);

				Set(cacheKey, itemList);
			}
		}

		/// <summary>
		/// Clear the cache from memory
		/// </summary>
		/// <param name="cacheKey"></param>
		public static void Clear(string cacheKey)
		{
			cache.Remove(cacheKey);
		}

		/// <summary>
		/// Clear all cache from memory
		/// </summary>
		/// <param name="cacheKey"></param>
		public static void ClearAll()
		{
			cache.Select(kvp => kvp.Key).ToList().ForEach(k => cache.Remove(k));
		}

		#endregion
	}
}
