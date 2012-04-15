using System;
using TTT.Core.Domain.Entities;

namespace TTT.Tests.Helpers.Builders
{
	public class GameBuilder
	{
		private Guid _id;

		public GameBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public Game Build()
		{
			var game = new Game
			{
				Id = _id
			};
			return game;
		}
	}
}