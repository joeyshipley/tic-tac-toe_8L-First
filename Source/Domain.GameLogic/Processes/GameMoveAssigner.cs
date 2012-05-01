using TTT.Domain.Entities;
using TTT.Domain.Factories;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Processes
{
	public class GameMoveAssigner : IGameMoveAssigner
	{
		private readonly IGameFactory _gameFactory;

		public GameMoveAssigner(IGameFactory gameFactory)
		{
			_gameFactory = gameFactory;
		}

		public void AssignMove(Game game, Enums.PlayerType owner, BoardPosition boardPosition, RepositoryDelegates.SaveGame saveGame)
		{
			var gameMove = _gameFactory.CreateFrom(owner, boardPosition);
			AssignMove(game, gameMove, saveGame);
		}

		public void AssignMove(Game game, GameMove gameMove, RepositoryDelegates.SaveGame saveGame)
		{
			game.AddMove(gameMove);
			saveGame(game);
		}
	}
}