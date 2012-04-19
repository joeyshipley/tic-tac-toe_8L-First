using System;
using System.Collections.Generic;
using System.Linq;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Providers;

namespace TTT.Core.Domain.Processes
{
	public class GameAlgorithms : IGameAlgorithms
	{
		private readonly IBoardPositionsProvider _boardPositionsProvider;

		public GameAlgorithms(IBoardPositionsProvider boardPositionsProvider)
		{
			_boardPositionsProvider = boardPositionsProvider;
		}

		public GameMove DetermineNextMove(Game game)
		{
			if(game.Moves.Count() == 1)
				return getFirstMove(game);

			var computerWinPositions = _boardPositionsProvider.GetPotentialWinningMovesFor(game, Enums.PlayerType.Computer);
			if(computerWinPositions.Any())
				return GameMove.CreateFrom(Enums.PlayerType.Computer, computerWinPositions.FirstOrDefault());

			var humanThreatPositions = _boardPositionsProvider.GetPotentialWinningMovesFor(game, Enums.PlayerType.Human);
			if(humanThreatPositions.Any())
				return GameMove.CreateFrom(Enums.PlayerType.Computer, humanThreatPositions.FirstOrDefault());

			// TODO: determine how to handle the Corner to Corner win pattern. 
			// NOTE: Should we weight the N/S/E/W as higher priority moves to make or possibly
			// determine if we are forcing the player to make a blocking move to keep playing, and if so
			// how many possible win moves would that create for the players. Then select a move from
			// the lowest win possibilities.

			var availablePositions = _boardPositionsProvider.GetRemainingAvailableBoardPositions(game);
			return getRandomMoveFromAvailable(availablePositions);
		}

		private GameMove getRandomMoveFromAvailable(IList<BoardPosition> availablePositions)
		{
			var random = new Random();
			var randomNumber = random.Next(0, availablePositions.Count() - 1);
			var randomPosition = availablePositions.ElementAt(randomNumber);
			return GameMove.CreateFrom(Enums.PlayerType.Computer, randomPosition);
		}

		private GameMove getFirstMove(Game game)
		{
			var move = getCenterPositionIfAvailable(game) 
				?? firstMoveCornerFallBackWhenCenterHasBeenTaken();
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