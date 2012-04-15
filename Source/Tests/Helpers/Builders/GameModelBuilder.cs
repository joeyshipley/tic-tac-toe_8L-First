using System;
using System.Collections.Generic;
using TTT.Core.Domain.Models;

namespace TTT.Tests.Helpers.Builders
{
	public class GameModelBuilder
	{
		private Guid _id;
		private IList<GameMoveModel> _moves;

		public GameModelBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public GameModelBuilder WithMoves(IList<GameMoveModel> moves)
		{
			_moves = moves;
			return this;
		}

		public GameModel Build()
		{
			var model = new GameModel
			{
				GameId = _id,
				GameMoves = _moves
			};
			return model;
		}

		public GameModel BuildNewGame()
		{
			_id = Guid.NewGuid();
			_moves = new List<GameMoveModel>();
			return Build();
		}
	}
}