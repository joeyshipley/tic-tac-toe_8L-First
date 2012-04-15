using System;
using System.Collections.Generic;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Factories
{
	public class GameFactory : IGameFactory
	{
		public Game CreateNew()
		{
			var game = new Game
			{
				Id = Guid.NewGuid(),
				Moves = new List<GameMove>()
			};
			return game;
		}
	}
}