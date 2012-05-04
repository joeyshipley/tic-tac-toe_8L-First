using System.Collections.Generic;
using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Providers
{
	public interface IAvailableBoardPositionsProvider
	{
		IList<BoardPosition> GetRemainingAvailableBoardPositions(Game game);
	}
}