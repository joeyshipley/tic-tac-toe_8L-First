using System;
using System.Collections.Generic;
using TTT.Domain.Entities;

namespace TTT.Domain.Factories
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