using System.Collections.Generic;
using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Providers
{
	public interface IWinningMoveProvider
	{
		IList<BoardPosition> GetPotentialWinningMovesFor(IList<GameMove> currentMoves, Enums.PlayerType owner);
	}
}