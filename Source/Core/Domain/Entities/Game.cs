using System;
using System.Collections.Generic;

namespace TTT.Core.Domain.Entities
{
	public class Game
	{
		public Game()
		{
			Moves = new List<GameMove>();
		}

		public Guid Id { get; set; }
		public IList<GameMove> Moves { get; set; }
	}
}