using System;

namespace TTT.Core.Domain.Entities
{
	public class GameMove
	{
		public Guid Id { get; set; }
		public Enums.PlayerType Owner { get; set; }
		public Enums.BoardPosition Position { get; set; }
	}
}