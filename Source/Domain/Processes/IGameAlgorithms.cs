using TTT.Domain.Entities;

namespace TTT.Domain.Processes
{
	public interface IGameAlgorithms
	{
		GameMove DetermineNextMove(Game game);
	}
}