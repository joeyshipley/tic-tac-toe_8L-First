using TTT.Domain.Entities;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Processes
{
	public interface IGameStatusAssigner
	{
		void AssignGameStatus(Game game, RepositoryDelegates.SaveGame saveGame);
	}
}