using System.Collections.Generic;
using TTT.Domain.Entities;
using TTT.Domain.Helpers;

namespace TTT.Domain.Providers
{
	public class WinningSetsProvider : IWinningSetsProvider
	{
		public IList<BoardPositionSet> GetWinningSets()
		{
			var sets = new List<BoardPositionSet>();

			// add rows
			for(var i = 1; i <= Constants.BoardPositionRowLength; i++)
			{
				var letter = i.ToAlphabet();
				sets.Add(new BoardPositionSet 
				{
					Positions = new List<BoardPosition>
					{
						BoardPosition.CreateFrom(letter, 1),
						BoardPosition.CreateFrom(letter, 2),
						BoardPosition.CreateFrom(letter, 3),
					}
				});
			}

			// add columns
			for(var i = 1; i <= Constants.BoardPositionRowLength; i++)
			{
				sets.Add(new BoardPositionSet 
				{
					Positions = new List<BoardPosition>
					{
						BoardPosition.CreateFrom("A", i),
						BoardPosition.CreateFrom("B", i),
						BoardPosition.CreateFrom("C", i),
					}
				});
			}

			// add top left to bottom right diagonal
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

			// add bottom left to top right diagonal
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

			return sets;
		}
	}
}