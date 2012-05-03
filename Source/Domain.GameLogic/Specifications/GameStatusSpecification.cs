using System.Linq;
using TTT.Domain.Entities;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Specifications
{
	public class GameStatusSpecification : IGameStatusSpecification
	{
		public bool IsGameOver(Game game)
		{
			// If the game is a draw, the game is over.
			if(game.Moves.Count == Constants.BoardPositionRowLength * Constants.BoardPositionRowLength)
				return true;

			// If the player won, the game is over.
			if(IsPlayerWinner(game))
				return true;

			// If the computer won, the game is over.
			if(IsComputerWinner(game))
				return true;

			// No game ending combinations were found, the game is not over.
			return false;
		}

		public bool IsPlayerWinner(Game game)
		{
			return isOwnerWinner(game, Enums.PlayerType.Human);
		}

		public bool IsComputerWinner(Game game)
		{
			return isOwnerWinner(game, Enums.PlayerType.Computer);
		}

		private bool isOwnerWinner(Game game, Enums.PlayerType owner)
		{
			var foundCompleteMatch = false;

			for(var columnValue = 1; columnValue <= Constants.BoardPositionRowLength; columnValue++)
			{
				var column = columnValue.ToAlphabet();
				for(var rowValue = 1; rowValue <= Constants.BoardPositionRowLength; rowValue++)
				{
					var row = rowValue;
					var currentPosition = BoardPosition.CreateFrom(column, row);

					var isInMoveListForOwner = checkCurrentBoardPositionForMoveAndOwner(game, currentPosition, owner);
					if(!isInMoveListForOwner)
						continue;

					// check to see if this column has a matching set for the owner
					var numberOfMovesInThisColumnBelongingToOwner = game.Moves.Count(m => m.Owner == owner && m.Position.Column == column);
					if(numberOfMovesInThisColumnBelongingToOwner == Constants.BoardPositionRowLength)
						foundCompleteMatch = true;

					// check to see if this row has a matching set for the owner
					var numberOfMovesInThisRowBelongingToOwner = game.Moves.Count(m => m.Owner == owner && m.Position.Row == row);
					if(numberOfMovesInThisRowBelongingToOwner == Constants.BoardPositionRowLength)
						foundCompleteMatch = true;
				}
			}

			// check top left to bottom right diagonal
			if(!foundCompleteMatch)
			{
				var isDiagonalMatch = true;
				for(var i = 1; i <= Constants.BoardPositionRowLength; i++)
				{
					var column = i.ToAlphabet();
					var currentDiagonalPosition = BoardPosition.CreateFrom(column, i);

					// has a move been set for this column/row
					var isInDiagonal = checkCurrentBoardPositionForMoveAndOwner(game, currentDiagonalPosition, owner);
					if(!isInDiagonal)
						isDiagonalMatch = false;
				}
				foundCompleteMatch = isDiagonalMatch;
			}

			// check bottom left to top right diagonal
			if(!foundCompleteMatch)
			{
				var isDiagonalMatch = true;
				for(var i = Constants.BoardPositionRowLength; i > 0; i--)
				{
					var column = i.ToAlphabet();
					var currentDiagonalPosition = BoardPosition.CreateFrom(column, Constants.BoardPositionRowLength - i + 1);

					// has a move been set for this column/row
					var isInDiagonal = checkCurrentBoardPositionForMoveAndOwner(game, currentDiagonalPosition, owner);
					if(!isInDiagonal)
						isDiagonalMatch = false;
				}
				foundCompleteMatch = isDiagonalMatch;
			}

			return foundCompleteMatch;
		}

		private bool checkCurrentBoardPositionForMoveAndOwner(Game game, BoardPosition position, Enums.PlayerType owner)
		{
			var move = game.Moves.FirstOrDefault(m => m.Position.Equals(position));
			return move != null 
				&& move.Owner == owner;
		}
	}
}