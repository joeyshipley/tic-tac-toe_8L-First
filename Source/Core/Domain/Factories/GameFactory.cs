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

		public GameMove CreateFrom(Enums.PlayerType owner, BoardPosition position)
		{
			var move = new GameMove
			{
				Id = Guid.NewGuid(),
				Owner = owner,
				Position = position
			};
			return move;
		}
	}
}