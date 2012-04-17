﻿using System.Collections.Generic;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Providers
{
	public interface IBoardPositionsProvider
	{
		IList<BoardPosition> GetRemainingAvailableBoardPositions(Game game);
		IList<BoardPosition> GetPotentialWinningMovesFor(Game game, Enums.PlayerType owner);
	}
}