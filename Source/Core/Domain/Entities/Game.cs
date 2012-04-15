using System;
using System.Collections.Generic;

namespace TTT.Core.Domain.Entities
{
	public class Game
	{
		public Guid Id { get; set; }
		public IList<GameMove> Moves { get; set; }
		public bool IsGameOver { get; set; }

		public void AddMove(GameMove move)
		{
			Moves.Add(move);
		}
	}
}