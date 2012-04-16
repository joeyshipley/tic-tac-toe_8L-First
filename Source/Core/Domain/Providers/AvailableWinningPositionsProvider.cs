using System.Collections.Generic;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Providers
{
	public class AvailableWinningPositionsProvider : IAvailableWinningPositionsProvider
	{
		private readonly IWinningSetsProvider _winningSetsProvider;

		public AvailableWinningPositionsProvider(IWinningSetsProvider winningSetsProvider)
		{
			_winningSetsProvider = winningSetsProvider;
		}

		public IList<BoardPosition> GetPotentialWinningMovesFor(Game game, Enums.PlayerType owner)
		{
			var winningMovePositions = new List<BoardPosition>();
			var possibleWinningSets = _winningSetsProvider.GetWinningSets();
			foreach(var set in possibleWinningSets)
			{
				// TODO: check the game moves against this set and see if the owner has 2 of the 3.
			}


			return winningMovePositions;
		}
	}
}