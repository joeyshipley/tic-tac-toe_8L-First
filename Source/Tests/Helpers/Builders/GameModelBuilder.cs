using System;
using System.Collections.Generic;
using TTT.Domain.Models;

namespace TTT.Tests.Helpers.Builders
{
	public class GameModelBuilder
	{
		private Guid _id;
		private IList<GameMoveModel> _moves;
		private bool _isGameOver;

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

		public GameModelBuilder WithIsGameOver(bool isGameOver)
		{
			_isGameOver = isGameOver;
			return this;
		}

		public GameModel Build()
		{
			var model = new GameModel
			{
				GameId = _id,
				GameMoves = _moves,
				IsGameOver = _isGameOver
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