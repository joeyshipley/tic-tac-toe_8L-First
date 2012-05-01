using System.Collections.Generic;
using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Validators
{
	public interface IMoveValidator
	{
		IList<ValidationError> ValidateMove(Game game, Enums.PlayerType owner, BoardPosition boardPosition);
	}
}