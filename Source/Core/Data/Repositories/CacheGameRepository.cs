using System;
using TTT.Core.Application.Infrastructure;
using TTT.Core.Application.Repositories;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Data.Repositories
{
	public class CacheGameRepository : IGameRepository
	{
		private readonly ICacheDataStorage _cacheDataStorage;

		public CacheGameRepository(ICacheDataStorage cacheDataStorage)
		{
			_cacheDataStorage = cacheDataStorage;
		}

		public Game Get(Guid id)
		{
			var cacheKey = getCacheKeyForGame(id);
			var game = _cacheDataStorage.Get<Game>(cacheKey);
			return game;
		}

		public void Save(Game game)
		{
			var cacheKey = getCacheKeyForGame(game.Id);
			_cacheDataStorage.Add(cacheKey, game);
		}

		public void Remove(Game game)
		{
			var cacheKey = getCacheKeyForGame(game.Id);
			_cacheDataStorage.Remove(cacheKey);
		}
		
		private string getCacheKeyForGame(Guid id)
		{
			return string.Format("Game_{0}", id);
		}
	}
}