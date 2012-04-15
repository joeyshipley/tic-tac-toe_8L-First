using System;
using System.Collections.Generic;
using TTT.Core.Domain.Entities;

namespace TTT.Tests.Helpers.Builders
{
	public class GameBuilder
	{
		private Guid _id;
		private IList<GameMove> _moves; 

		public GameBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public GameBuilder WithMoves(IList<GameMove> moves)
		{
			_moves = moves;
			return this;
		}

		public Game Build()
		{
			var game = new Game
			{
				Id = _id,
				Moves = _moves
			};
			return game;
		}
	}
}