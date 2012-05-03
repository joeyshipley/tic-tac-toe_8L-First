using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Specifications
{
	public interface IGameSpecifications
	{
		bool IsGameOver(Game game);
		bool IsPlayerWinner(Game game);
		bool IsComputerWinner(Game game);
	}
}