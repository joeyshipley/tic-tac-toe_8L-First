using System.Collections.Generic;
using TTT.Domain.Entities;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Providers
{
	public class WinningSetsProvider : IWinningSetsProvider
	{
		public IList<BoardPositionSet> GetWinningSets()
		{
			var sets = new List<BoardPositionSet>();

			addWinningRowSets(sets);

			addWinningColumnSets(sets);
			
			addWinningTopLeftToBottomRightDiagonal(sets);

			addWinningBotomLeftToTopRightDiagonal(sets);
			
			return sets;
		}

		private void addWinningTopLeftToBottomRightDiagonal(IList<BoardPositionSet> sets)
		{
			var topLeftToBottomRightDiagonalPositions = new List<BoardPosition>();
			for(var i = 1; i <= Constants.BoardPositionRowLength; i++)
			{
				var column = i.ToAlphabet();
				var currentDiagonalPosition = BoardPosition.CreateFrom(column, i);
				topLeftToBottomRightDiagonalPositions.Add(currentDiagonalPosition);
			}
			sets.Add(new BoardPositionSet 
			{
				Positions = topLeftToBottomRightDiagonalPositions
			});
		}

		private void addWinningBotomLeftToTopRightDiagonal(IList<BoardPositionSet> sets)
		{
			var bottomLeftToTopRightDiagonalPositions = new List<BoardPosition>();
			for(var i = Constants.BoardPositionRowLength; i > 0; i--)
			{
				var column = i.ToAlphabet();
				var currentDiagonalPosition = BoardPosition.CreateFrom(column, Constants.BoardPositionRowLength - i + 1);
				bottomLeftToTopRightDiagonalPositions.Add(currentDiagonalPosition);
			}
			sets.Add(new BoardPositionSet 
			{
				Positions = bottomLeftToTopRightDiagonalPositions
			});
		}

		private void addWinningRowSets(IList<BoardPositionSet> sets)
		{
			// A to ? (max board length)
			for(var i = 1; i <= Constants.BoardPositionRowLength; i++)
			{
				var letter = i.ToAlphabet();
				var positions = new List<BoardPosition>();
				// 1 to ? (max board length)
				for(var j = 1; j <= Constants.BoardPositionRowLength; j++)
					positions.Add(BoardPosition.CreateFrom(letter, j));
				sets.Add(new BoardPositionSet 
				{
					Positions = positions
				});
			}
		}

		private void addWinningColumnSets(IList<BoardPositionSet> sets)
		{
			// 1 to ? (max board length)
			for(var i = 1; i <= Constants.BoardPositionRowLength; i++)
			{
				var positions = new List<BoardPosition>();
				// A to ? (max board length)
				for(var j = 1; j <= Constants.BoardPositionRowLength; j++)
					positions.Add(BoardPosition.CreateFrom(j.ToAlphabet(), i));
				sets.Add(new BoardPositionSet 
				{
					Positions = positions
				});
			}
		}
	}
}