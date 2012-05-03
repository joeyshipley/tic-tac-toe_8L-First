using System.Linq;
using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Specifications
{
	public class MoveValidationSpecification : IMoveValidationSpecification
	{
		public bool IsMoveValid(Game game, Enums.PlayerType owner, BoardPosition position)
		{
			return game.Moves.All(m => !m.Position.Equals(position));
		}
	}
}