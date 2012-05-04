using System.Collections.Generic;
using System.Linq;
using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Providers
{
	public class WinningMoveProvider : IWinningMoveProvider
	{
		private readonly IWinningSetsProvider _winningSetsProvider;

		public WinningMoveProvider(IWinningSetsProvider winningSetsProvider)
		{
			_winningSetsProvider = winningSetsProvider;
		}

		public IList<BoardPosition> GetPotentialWinningMovesFor(IList<GameMove> currentMoves, Enums.PlayerType owner)
		{
			var winningMovePositions = new List<BoardPosition>();
			var possibleWinningSets = _winningSetsProvider.GetWinningSets();
			var ownersPositions = new List<BoardPosition>();
			var gameMoves = currentMoves.Where(m => m.Owner == owner).ToList();
			gameMoves.ForEach(m => ownersPositions.Add(m.Position));
	
			possibleWinningSets.ToList().ForEach(set =>
			{
				var hasPotentialWinningPosition = ownersPositions.Count(m => set.Positions.Any(p => p.Equals(m))) == Constants.BoardPositionRowLength - 1;
				if(hasPotentialWinningPosition)
					set.Positions.ToList().ForEach(p => 
					{
						var hasAlreadyBeenChoosen = currentMoves.Any(m => m.Position.Equals(p));
						if(!hasAlreadyBeenChoosen)
							winningMovePositions.Add(p);
					});
			});
			return winningMovePositions;
		}
	}
}