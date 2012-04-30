using System.Collections.Generic;
using TTT.Domain.Entities;

namespace TTT.Domain.Providers
{
	public interface IBoardPositionsProvider
	{
		IList<BoardPosition> GetRemainingAvailableBoardPositions(Game game);
		IList<BoardPosition> GetPotentialWinningMovesFor(IList<GameMove> currentMoves, Enums.PlayerType owner);
	}
}