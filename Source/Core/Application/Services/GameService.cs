using TTT.Core.Application.Factories;
using TTT.Core.Application.Repositories;
using TTT.Core.Domain.Factories;
using TTT.Core.Domain.Models;

namespace TTT.Core.Application.Services
{
	public class GameService : IGameService
	{
		private readonly IGameFactory _gameFactory;
		private readonly IModelFactory _modelFactory;
		private readonly IGameRepository _gameRepository;

		public GameService(IGameFactory gameFactory, IModelFactory modelFactory, IGameRepository gameRepository)
		{
			_gameFactory = gameFactory;
			_modelFactory = modelFactory;
			_gameRepository = gameRepository;
		}

		public GameModel New()
		{
			var game = _gameFactory.CreateNew();
			_gameRepository.Save(game);
			var model = _modelFactory.CreateFrom(game);
			return model;
		}
	}
}