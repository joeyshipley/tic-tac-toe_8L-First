using System.Collections.Generic;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Specifications;

namespace TTT.Domain.GameLogic.Validators
{
	public class MoveValidator : IMoveValidator
	{
		private readonly IMoveValidationSpecification _moveValidationSpecification;

		public MoveValidator(IMoveValidationSpecification moveValidationSpecification)
		{
			_moveValidationSpecification = moveValidationSpecification;
		}

		public IList<ValidationError> ValidateMove(Game game, Enums.PlayerType owner, BoardPosition boardPosition)
		{
			var warnings = new List<ValidationError>();
			var isMoveLegitimate = _moveValidationSpecification.IsMoveValid(game, owner, boardPosition);

			if(!isMoveLegitimate)
				warnings.Add(new ValidationError { Type = "InvalidMove", Message = "Sorry this move is not legal, try another move to keep playing." });

			return warnings;
		}
	}
}