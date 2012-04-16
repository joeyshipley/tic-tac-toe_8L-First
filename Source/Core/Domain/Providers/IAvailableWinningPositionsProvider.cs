using System.Collections.Generic;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Providers
{
	public interface IAvailableWinningPositionsProvider
	{
		IList<BoardPosition> GetPotentialWinningMovesFor(Game game, Enums.PlayerType owner);
	}
}