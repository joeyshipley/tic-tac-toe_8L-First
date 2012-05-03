using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Specifications;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Processes
{
	public class GameStatusAssigner : IGameStatusAssigner
	{
		private readonly IGameStatusSpecification _gameStatusSpecifications;

		public GameStatusAssigner(IGameStatusSpecification gameStatusSpecifications)
		{
			_gameStatusSpecifications = gameStatusSpecifications;
		}

		public void AssignGameStatus(Game game, RepositoryDelegates.SaveGame saveGame)
		{
			var isGameOver = _gameStatusSpecifications.IsGameOver(game);
			game.IsGameOver = isGameOver;
			saveGame(game);
		}
	}
}