using System;
using System.Linq;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Processes
{
	public class GameAlgorithms : IGameAlgorithms
	{
		public GameMove DetermineNextMove(Game game)
		{
			if(game.Moves.Count() == 1)
				return getFirstMove(game);

			GameMove move = null;
			return move;
		}

		private GameMove getFirstMove(Game game)
		{
			var move = getCenterPositionIfAvailable(game) 
				?? firstMoveCornerFallBackWhenCenterHasBeenTaken();

			// TODO: check to see if there are any player winning positions that should be taken
			// ----- use the AvailableWinningPositionsProvider for this.

			// TODO: check to see if there are any computer winning positions that should be taken
			// ----- use the AvailableWinningPositionsProvider for this.

			// TODO: if none have been found, randomly select from the available positions.

			return move;
		}

		private GameMove getCenterPositionIfAvailable(Game game)
		{
			var centerPosition = BoardPosition.CreateFrom("B", 2);
			var centerIsAlreadySelected = game.Moves.Any(m => m.Position.Equals(centerPosition));
			return !centerIsAlreadySelected 
				? GameMove.CreateFrom(Enums.PlayerType.Computer, centerPosition) 
				: null;
		}

		private GameMove firstMoveCornerFallBackWhenCenterHasBeenTaken()
		{
			var random = new Random();
			var randomNumber = random.Next(1, 4);
			switch(randomNumber)
			{
				case 1:
					return GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom("A", 1));
				case 2:
					return GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom("A", 3));
				case 3:
					return GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom("C", 1));
				case 4:
				default:
					return GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom("C", 3));
			}
		}
	}
}