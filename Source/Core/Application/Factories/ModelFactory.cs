using System.Collections.Generic;
using System.Linq;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Models;

namespace TTT.Core.Application.Factories
{
	public class ModelFactory : IModelFactory
	{
		public GameModel CreateFrom(Game game)
		{
			var moves = createMoveModelsFromGame(game);
			var model = new GameModel
			{
				GameId = game.Id,
				GameMoves = moves
			};
			return model;
		}

		private IList<GameMoveModel> createMoveModelsFromGame(Game game)
		{
			var moves = game.Moves.Select(move => 
			{
				var moveModel = new GameMoveModel
				{
					Owner = move.Owner,
					Position = move.Position
				};
				return moveModel;
			}).ToList();
			return moves;
		}
	}
}