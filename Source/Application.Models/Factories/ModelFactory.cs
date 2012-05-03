using System.Collections.Generic;
using System.Linq;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Specifications;

namespace TTT.Application.Models.Factories
{
	public class ModelFactory : IModelFactory
	{
		private readonly IGameStatusSpecification _gameStatusSpecification;

		public ModelFactory(IGameStatusSpecification gameStatusSpecification)
		{
			_gameStatusSpecification = gameStatusSpecification;
		}

		public GameModel CreateFrom(Game game)
		{
			return CreateFrom(game, new List<ValidationError>());
		}

		public GameModel CreateFrom(Game game, IList<ValidationError> moveWarnings)
		{
			var moves = createMoveModelsFromGame(game);
			var model = new GameModel
			{
				GameId = game.Id,
				GameMoves = moves,
				IsGameOver = game.IsGameOver,
				MoveWarnings = moveWarnings
			};

			var isComputerWinner = game.IsGameOver && _gameStatusSpecification.IsComputerWinner(game);
			model.IsComputerWinner = isComputerWinner;

			var isPlayerWinner = game.IsGameOver && _gameStatusSpecification.IsPlayerWinner(game);
			model.IsPlayerWinner = isPlayerWinner;

			var isGameDraw = game.IsGameOver && !isComputerWinner && !isPlayerWinner;
			model.IsGameDraw = isGameDraw;

			return model;
		}

		private IList<GameMoveModel> createMoveModelsFromGame(Game game)
		{
			var moves = game.Moves.Select(move => 
			{
				var position = BoardPositionModel.CreateFrom(move.Position);
				var moveModel = new GameMoveModel
				{
					Owner = move.Owner.ToString(),
					Position = position
				};
				return moveModel;
			}).ToList();
			return moves;
		}
	}
}