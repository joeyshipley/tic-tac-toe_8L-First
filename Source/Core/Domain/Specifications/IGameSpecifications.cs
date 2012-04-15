using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Specifications
{
	public interface IGameSpecifications
	{
		bool IsMoveLegitimate(Game game, Enums.PlayerType owner, Enums.BoardPosition position);
		bool IsGameOver(Game game);
	}
}