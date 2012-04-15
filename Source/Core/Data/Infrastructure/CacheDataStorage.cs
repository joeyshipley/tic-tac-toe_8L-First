using System;
using System.Runtime.Caching;
using TTT.Core.Application.Infrastructure;

namespace TTT.Core.Data.Infrastructure
{
	public class CacheDataStorage : ICacheDataStorage
	{
		private static readonly ObjectCache _cache = MemoryCache.Default;
		private readonly CacheItemPolicy _defaultPolicty = new CacheItemPolicy();

		public bool Exists(string key)
		{
			return _cache[key] != null;
		}

		public T Get<T>(string key)
		{
			var cacheValue = _cache[key];
			if(cacheValue == null)
				return default(T);

			return (T) Convert.ChangeType(cacheValue, typeof(T)); // return stored value.
		}

		public void Add<T>(string key, T dataObject)
		{
			Add(key, dataObject, _defaultPolicty);
		}

		public void Add<T>(string key, T dataObject, CacheItemPolicy policy)
		{
			_cache.Set(key, dataObject, policy);
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}
	}
}