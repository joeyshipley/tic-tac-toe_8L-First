using System.Collections.Generic;
using System.Linq;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Providers;

namespace TTT.Core.Domain.Processes
{
	public class GameAlgorithms : IGameAlgorithms
	{
		private readonly IBoardPositionsProvider _boardPositionsProvider;
		private readonly IRandomNumberProvider _randomNumberProvider;

		public GameAlgorithms(IBoardPositionsProvider boardPositionsProvider, IRandomNumberProvider randomNumberProvider)
		{
			_boardPositionsProvider = boardPositionsProvider;
			_randomNumberProvider = randomNumberProvider;
		}

		public GameMove DetermineNextMove(Game game)
		{
			if(game.Moves.Count() == 1)
				return getFirstMove(game);

			var currentMoves = game.Moves;
	
			var computerWinPositions = _boardPositionsProvider.GetPotentialWinningMovesFor(currentMoves, Enums.PlayerType.Computer);
			if(computerWinPositions.Any())
				return GameMove.CreateFrom(Enums.PlayerType.Computer, computerWinPositions.FirstOrDefault());

			var humanThreatPositions = _boardPositionsProvider.GetPotentialWinningMovesFor(currentMoves, Enums.PlayerType.Human);
			if(humanThreatPositions.Any())
				return GameMove.CreateFrom(Enums.PlayerType.Computer, humanThreatPositions.FirstOrDefault());

			// TODO: determine how to handle the Corner to Corner win pattern. 
			// NOTE: Should we weight the N/S/E/W as higher priority moves to make or possibly
			// determine if we are forcing the player to make a blocking move to keep playing, and if so
			// how many possible win moves would that create for the players. Then select a move from
			// the lowest win possibilities.

			var availablePositions = _boardPositionsProvider.GetRemainingAvailableBoardPositions(game);
			availablePositions = removePositionsThatWouldForceThePlayerToBlockAndCreateDoubleWinningMoves(currentMoves, availablePositions);
			return getRandomMoveFromAvailable(availablePositions);
		}

		private IList<BoardPosition> removePositionsThatWouldForceThePlayerToBlockAndCreateDoubleWinningMoves(IList<GameMove> currentMoves, IList<BoardPosition> availablePositions)
		{
			var responsePositions = new List<BoardPosition>();
			foreach(var position in availablePositions)
			{
				// check the potential move to see if it creates a need for the player to block it.
				var newMovesAfterComputerHasChoosen = new List<GameMove>(currentMoves) 
				{
					GameMove.CreateFrom(Enums.PlayerType.Computer, position)
				};
				var computerWinningMoves = _boardPositionsProvider.GetPotentialWinningMovesFor(newMovesAfterComputerHasChoosen, Enums.PlayerType.Computer);
				foreach(var winningPositions in computerWinningMoves)
				{
					// apply the players blocking move and check to see if that move creates two winning positions
					var newMovesAfterPlayerHasChoosen = new List<GameMove>(newMovesAfterComputerHasChoosen)
					{
						GameMove.CreateFrom(Enums.PlayerType.Human, winningPositions)
					};
					var playerWinningMoves = _boardPositionsProvider.GetPotentialWinningMovesFor(newMovesAfterPlayerHasChoosen, Enums.PlayerType.Human);
					if(playerWinningMoves.Count() < 2)
						// if the potential move does not cause the player to block and create two
						// winning positions, add it to the acceptable move list.
						responsePositions.Add(position);
				}
			}
			return responsePositions;
		}

		private GameMove getRandomMoveFromAvailable(IList<BoardPosition> availablePositions)
		{
			var randomNumber = _randomNumberProvider.GenerateNumber(0, availablePositions.Count() - 1);
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
			// TODO: determine center position from logic, not hard coding.
			var centerPosition = BoardPosition.CreateFrom("B", 2);
			var centerIsAlreadySelected = game.Moves.Any(m => m.Position.Equals(centerPosition));
			return !centerIsAlreadySelected 
				? GameMove.CreateFrom(Enums.PlayerType.Computer, centerPosition) 
				: null;
		}

		private GameMove firstMoveCornerFallBackWhenCenterHasBeenTaken()
		{
			// TODO: build this from logic instead of hardcoded case statement.
			var randomNumber = _randomNumberProvider.GenerateNumber(1, 4);
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