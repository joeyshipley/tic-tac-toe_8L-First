using System.Runtime.Caching;

namespace TTT.Core.Application.Infrastructure
{
	public interface ICacheDataStorage
	{
		bool Exists(string key);
		T Get<T>(string key);
		void Add<T>(string key, T dataObject);
		void Add<T>(string key, T dataObject, CacheItemPolicy policy);
		void Remove(string key);
	}
}