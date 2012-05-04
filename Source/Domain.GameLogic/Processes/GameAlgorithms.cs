using System.Collections.Generic;
using System.Linq;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Providers;
using TTT.Domain.GameLogic.Specifications;

namespace TTT.Domain.GameLogic.Processes
{
	public class GameAlgorithms : IGameAlgorithms
	{
		private readonly IAvailableBoardPositionsProvider _availableBoardPositionsProvider;
		private readonly IRandomNumberProvider _randomNumberProvider;
		private readonly IWinningMoveProvider _winningMoveProvider;
		private readonly IComputerFirstTurnMoveProvider _computerFirstTurnMoveProvider;
		private readonly IComputerFirstTurnSpecification _computerFirstTurnSpecification;

		public GameAlgorithms(IAvailableBoardPositionsProvider availableBoardPositionsProvider, 
			IRandomNumberProvider randomNumberProvider,
			IWinningMoveProvider winningMoveProvider,
			IComputerFirstTurnMoveProvider computerFirstTurnMoveProvider,
			IComputerFirstTurnSpecification computerFirstTurnSpecification)
		{
			_availableBoardPositionsProvider = availableBoardPositionsProvider;
			_randomNumberProvider = randomNumberProvider;
			_winningMoveProvider = winningMoveProvider;
			_computerFirstTurnMoveProvider = computerFirstTurnMoveProvider;
			_computerFirstTurnSpecification = computerFirstTurnSpecification;
		}

		public GameMove DetermineNextMove(Game game)
		{
			if(_computerFirstTurnSpecification.IsFirstTurnForComputer(game))
				return getComputersFirstMove(game);

			// if the computer can win this, do it!
			var computerWinPositions = _winningMoveProvider.GetPotentialWinningMovesFor(game.Moves, Enums.PlayerType.Computer);
			if(computerWinPositions.Any())
				return GameMove.CreateFrom(Enums.PlayerType.Computer, computerWinPositions.FirstOrDefault());

			// if the human can potentially win, lets block it.
			var humanThreatPositions = _winningMoveProvider.GetPotentialWinningMovesFor(game.Moves, Enums.PlayerType.Human);
			if(humanThreatPositions.Any())
				return GameMove.CreateFrom(Enums.PlayerType.Computer, humanThreatPositions.FirstOrDefault());

			return returnRandomSafeMoveFromAvailableMoves(game, game.Moves);
		}

		private IList<BoardPosition> removePositionsThatWouldForceThePlayerToBlockAndCreateDoubleWinningMoves(IList<GameMove> currentMoves, IList<BoardPosition> availablePositions)
		{
			var responsePositions = new List<BoardPosition>();
			foreach(var position in availablePositions)
			{
				var newMovesAfterComputerHasChoosen = returnGameMovesWithPossibleComputerMove(currentMoves, position);
				var computerWinningMoves = _winningMoveProvider.GetPotentialWinningMovesFor(newMovesAfterComputerHasChoosen, Enums.PlayerType.Computer);

				foreach(var winningPosition in computerWinningMoves)
				{
					var newMovesAfterPlayerHasChoosen = returnGameMovesWithNextPotentialPlayerMove(newMovesAfterComputerHasChoosen, winningPosition);
					var playerWinningMoves = _winningMoveProvider.GetPotentialWinningMovesFor(newMovesAfterPlayerHasChoosen, Enums.PlayerType.Human);
					applyMoveIfItDoesNotCreateMultipleWinningMovesForThePlayer(responsePositions, playerWinningMoves, position);
				}
			}
			return returnGameMovesFromDoubleWinningRemovalProcess(availablePositions, responsePositions);
		}

		private IList<BoardPosition> returnGameMovesFromDoubleWinningRemovalProcess(IList<BoardPosition> availablePositions, IList<BoardPosition> scrubbedPositions)
		{
			return scrubbedPositions.Any() 
				? scrubbedPositions
				: availablePositions; // if there are no safe moves, suck it up, send back what is available.
		}

		private List<GameMove> returnGameMovesWithPossibleComputerMove(IList<GameMove> currentMoves, BoardPosition potentialBoardPosition)
		{
			var newMovesAfterComputerHasChoosen = new List<GameMove>(currentMoves) 
			{
				GameMove.CreateFrom(Enums.PlayerType.Computer, potentialBoardPosition)
			};
			return newMovesAfterComputerHasChoosen;

		}

		private List<GameMove> returnGameMovesWithNextPotentialPlayerMove(IList<GameMove> newMovesAfterComputerHasChoosen, BoardPosition playerWinningMove)
		{
			var newMovesAfterPlayerHasChoosen = new List<GameMove>(newMovesAfterComputerHasChoosen)
			{
				GameMove.CreateFrom(Enums.PlayerType.Human, playerWinningMove)
			};
			return newMovesAfterPlayerHasChoosen;
		}

		private void applyMoveIfItDoesNotCreateMultipleWinningMovesForThePlayer(IList<BoardPosition> responsePositions, IList<BoardPosition> playerWinningMoves, BoardPosition position)
		{
			if(playerWinningMoves.Count() < 2)
				responsePositions.Add(position);
		}

		private GameMove getRandomMoveFromAvailable(IList<BoardPosition> availablePositions)
		{
			var randomNumber = _randomNumberProvider.GenerateNumber(0, availablePositions.Count() - 1);
			var randomPosition = availablePositions.ElementAt(randomNumber);
			return GameMove.CreateFrom(Enums.PlayerType.Computer, randomPosition);
		}

		private GameMove getComputersFirstMove(Game game)
		{
			var move = getCenterPositionIfAvailable(game) 
				?? firstMoveCornerFallBackWhenCenterHasBeenTaken();
			return move;
		}

		private GameMove getCenterPositionIfAvailable(Game game)
		{
			var centerMove = _computerFirstTurnMoveProvider.GetCenterSquareMove();
			var centerMovePosition = centerMove.Position;
			var centerIsAlreadySelected = game.Moves.Any(m => m.Position.Equals(centerMovePosition));
			return !centerIsAlreadySelected 
				? centerMove 
				: null;
		}

		private GameMove firstMoveCornerFallBackWhenCenterHasBeenTaken()
		{
			var cornerMoves = _computerFirstTurnMoveProvider.GetCornerMoves();
			var randomNumber = _randomNumberProvider.GenerateNumber(0, 3);
			return cornerMoves.ElementAt(randomNumber);
		}

		private GameMove returnRandomSafeMoveFromAvailableMoves(Game game, IList<GameMove> currentMoves)
		{
			var availablePositions = _availableBoardPositionsProvider.GetRemainingAvailableBoardPositions(game);

			// lets make sure we don't return a move that a player has to block, and it creates a double move win scenerio for them.
			availablePositions = removePositionsThatWouldForceThePlayerToBlockAndCreateDoubleWinningMoves(currentMoves, availablePositions);
			
			return getRandomMoveFromAvailable(availablePositions);
		}
	}
}