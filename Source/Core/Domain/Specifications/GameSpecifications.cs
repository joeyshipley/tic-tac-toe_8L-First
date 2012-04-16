using System.Linq;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Specifications
{
	public class GameSpecifications : IGameSpecifications
	{
		public bool IsMoveLegitimate(Game game, Enums.PlayerType owner, Enums.BoardPosition position)
		{
			return game.Moves.All(m => m.Position != position);
		}

		public bool IsGameOver(Game game)
		{
			// If the game is a draw, the game is over.
			if(game.Moves.Count == Constants.MaxNumberOfPositionsAvailable)
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
			return hasTopRowMatch(game, owner)
				|| hasMiddleRowMatch(game, owner)
				|| hasBottomRowMatch(game, owner)
				|| hasLeftColumnMatch(game, owner)
				|| hasCenterColumnMatch(game, owner)
				|| hasRightColumnMatch(game, owner)
				|| hasTopLeftDiagonalMatch(game, owner)
				|| hasBottomLeftDiagonalMatch(game, owner);
		}

		private bool hasTopRowMatch(Game game, Enums.PlayerType owner)
		{
			var ownersSelections = game.Moves.Where(m => m.Owner == owner);
			var numberInSet = ownersSelections.Count(m => 
				m.Position == Enums.BoardPosition.TopLeft
				|| m.Position == Enums.BoardPosition.TopCenter
				|| m.Position == Enums.BoardPosition.TopRight
			);
			return numberInSet == Constants.NumberInSetToWin;
		}
		
		private bool hasMiddleRowMatch(Game game, Enums.PlayerType owner)
		{
			var ownersSelections = game.Moves.Where(m => m.Owner == owner);
			var numberInSet = ownersSelections.Count(m => 
				m.Position == Enums.BoardPosition.MiddleLeft
				|| m.Position == Enums.BoardPosition.MiddleCenter
				|| m.Position == Enums.BoardPosition.MiddleRight
			);
			return numberInSet == Constants.NumberInSetToWin;
		}
		
		private bool hasBottomRowMatch(Game game, Enums.PlayerType owner)
		{
			var ownersSelections = game.Moves.Where(m => m.Owner == owner);
			var numberInSet = ownersSelections.Count(m => 
				m.Position == Enums.BoardPosition.BottomLeft
				|| m.Position == Enums.BoardPosition.BottomCenter
				|| m.Position == Enums.BoardPosition.BottomRight
			);
			return numberInSet == Constants.NumberInSetToWin;
		}

		private bool hasLeftColumnMatch(Game game, Enums.PlayerType owner)
		{
			var ownersSelections = game.Moves.Where(m => m.Owner == owner);
			var numberInSet = ownersSelections.Count(m => 
				m.Position == Enums.BoardPosition.TopLeft
				|| m.Position == Enums.BoardPosition.MiddleLeft
				|| m.Position == Enums.BoardPosition.BottomLeft
			);
			return numberInSet == Constants.NumberInSetToWin;
		}
		
		private bool hasCenterColumnMatch(Game game, Enums.PlayerType owner)
		{
			var ownersSelections = game.Moves.Where(m => m.Owner == owner);
			var numberInSet = ownersSelections.Count(m => 
				m.Position == Enums.BoardPosition.TopCenter
				|| m.Position == Enums.BoardPosition.MiddleCenter
				|| m.Position == Enums.BoardPosition.BottomCenter
			);
			return numberInSet == Constants.NumberInSetToWin;
		}
		
		private bool hasRightColumnMatch(Game game, Enums.PlayerType owner)
		{
			var ownersSelections = game.Moves.Where(m => m.Owner == owner);
			var numberInSet = ownersSelections.Count(m => 
				m.Position == Enums.BoardPosition.TopRight
				|| m.Position == Enums.BoardPosition.MiddleRight
				|| m.Position == Enums.BoardPosition.BottomRight
			);
			return numberInSet == Constants.NumberInSetToWin;
		}
		
		private bool hasTopLeftDiagonalMatch(Game game, Enums.PlayerType owner)
		{
			var ownersSelections = game.Moves.Where(m => m.Owner == owner);
			var numberInSet = ownersSelections.Count(m => 
				m.Position == Enums.BoardPosition.TopLeft
				|| m.Position == Enums.BoardPosition.MiddleCenter
				|| m.Position == Enums.BoardPosition.BottomRight
			);
			return numberInSet == Constants.NumberInSetToWin;
		}
		
		private bool hasBottomLeftDiagonalMatch(Game game, Enums.PlayerType owner)
		{
			var ownersSelections = game.Moves.Where(m => m.Owner == owner);
			var numberInSet = ownersSelections.Count(m => 
				m.Position == Enums.BoardPosition.BottomLeft
				|| m.Position == Enums.BoardPosition.MiddleCenter
				|| m.Position == Enums.BoardPosition.TopRight
			);
			return numberInSet == Constants.NumberInSetToWin;
		}
	}
}