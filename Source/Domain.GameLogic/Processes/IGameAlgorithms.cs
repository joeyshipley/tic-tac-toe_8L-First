using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Processes
{
	public interface IGameAlgorithms
	{
		GameMove DetermineNextMove(Game game);
	}
}