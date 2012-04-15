using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Processes
{
	public interface IGameAlgorithms
	{
		GameMove DetermineNextMove(Game game);
	}
}