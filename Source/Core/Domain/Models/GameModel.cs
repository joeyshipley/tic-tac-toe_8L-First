using System;
using System.Collections.Generic;

namespace TTT.Core.Domain.Models
{
	public class GameModel
	{
		public Guid GameId { get; set; }
		public IList<GameMoveModel> GameMoves { get; set; }
	}
}