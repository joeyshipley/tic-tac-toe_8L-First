using TTT.Domain.Entities;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Processes
{
	public interface IGameMoveAssigner
	{
		void AssignMove(Game game, Enums.PlayerType owner, BoardPosition boardPosition, RepositoryDelegates.SaveGame saveGame);
		void AssignMove(Game game, GameMove gameMove, RepositoryDelegates.SaveGame saveGame);
	}
}