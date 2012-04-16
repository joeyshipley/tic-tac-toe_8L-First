﻿using System.Collections.Generic;
using System.Linq;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Models;

namespace TTT.Core.Application.Factories
{
	public class ModelFactory : IModelFactory
	{
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
			return model;
		}

		private IList<GameMoveModel> createMoveModelsFromGame(Game game)
		{
			var moves = game.Moves.Select(move => 
			{
				var position = BoardPositionModel.CreateFrom(move.Position);;
				var moveModel = new GameMoveModel
				{
					Owner = move.Owner,
					Position = position
				};
				return moveModel;
			}).ToList();
			return moves;
		}
	}
}