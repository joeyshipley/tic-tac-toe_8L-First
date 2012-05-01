using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Specifications;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Processes
{
	public class GameStatusAssigner : IGameStatusAssigner
	{
		private readonly IGameSpecifications _gameSpecifications;

		public GameStatusAssigner(IGameSpecifications gameSpecifications)
		{
			_gameSpecifications = gameSpecifications;
		}

		public void AssignGameStatus(Game game, RepositoryDelegates.SaveGame saveGame)
		{
			var isGameOver = _gameSpecifications.IsGameOver(game);
			game.IsGameOver = isGameOver;
			saveGame(game);
		}
	}
}