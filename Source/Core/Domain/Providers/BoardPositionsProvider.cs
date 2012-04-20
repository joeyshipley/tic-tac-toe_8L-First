using System.Collections.Generic;
using System.Linq;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Helpers;

namespace TTT.Core.Domain.Providers
{
	public class BoardPositionsProvider : IBoardPositionsProvider
	{
		private readonly IWinningSetsProvider _winningSetsProvider;

		public BoardPositionsProvider(IWinningSetsProvider winningSetsProvider)
		{
			_winningSetsProvider = winningSetsProvider;
		}

		public IList<BoardPosition> GetRemainingAvailableBoardPositions(Game game)
		{
			var positions = getAllPossibleBoardPositions();
			var previouslySelectedPositions = game.Moves.Select(m => m.Position).ToList();
			previouslySelectedPositions.ForEach(p => positions.Remove(p));

			return positions;
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

		private IList<BoardPosition> getAllPossibleBoardPositions()
		{
			var allPossibleBoardPositions = new List<BoardPosition>();
			for(var i = 1; i <= Constants.BoardPositionRowLength; i++)
				for(var j = 1; j <= Constants.BoardPositionRowLength; j++)
					allPossibleBoardPositions.Add(BoardPosition.CreateFrom(i.ToAlphabet(), j));
			return allPossibleBoardPositions;
		}
	}
}