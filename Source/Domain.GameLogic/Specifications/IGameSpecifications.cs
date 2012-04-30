using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Specifications
{
	public interface IGameSpecifications
	{
		bool IsMoveLegitimate(Game game, Enums.PlayerType owner, BoardPosition position);
		bool IsGameOver(Game game);
		bool IsPlayerWinner(Game game);
		bool IsComputerWinner(Game game);
	}
}