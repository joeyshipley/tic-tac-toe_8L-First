using System.Collections.Generic;
using System.Linq;
using TTT.Domain.Entities;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Providers
{
	public class AvailableBoardPositionsProvider : IAvailableBoardPositionsProvider
	{
		public IList<BoardPosition> GetRemainingAvailableBoardPositions(Game game)
		{
			var positions = getAllPossibleBoardPositions();
			var previouslySelectedPositions = game.Moves.Select(m => m.Position).ToList();
			previouslySelectedPositions.ForEach(p => positions.Remove(p));

			return positions;
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