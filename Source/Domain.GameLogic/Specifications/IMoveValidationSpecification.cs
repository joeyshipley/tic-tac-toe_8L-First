using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Specifications
{
	public interface IMoveValidationSpecification
	{
		bool IsMoveValid(Game game, Enums.PlayerType owner, BoardPosition position);
	}
}