using System.Collections.Generic;
using System.Linq;
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
			var ownersPositions = new List<BoardPosition>();
			var gameMoves = game.Moves.Where(m => m.Owner == owner).ToList();
			gameMoves.ForEach(m => ownersPositions.Add(m.Position));
	
			possibleWinningSets.ToList().ForEach(set =>
			{
				var hasPotentialWinningPosition = ownersPositions.Count(m => set.Positions.Any(p => p.Equals(m))) == Constants.BoardPositionRowLength - 1;
				if(hasPotentialWinningPosition)
					set.Positions.ToList().ForEach(p => 
					{
						var hasAlreadyBeenChoosen = game.Moves.Any(m => m.Position.Equals(p));
						if(!hasAlreadyBeenChoosen)
							winningMovePositions.Add(p);
					});
			});
			return winningMovePositions;
		}
	}
}