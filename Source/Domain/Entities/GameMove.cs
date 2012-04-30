using System;

namespace TTT.Domain.Entities
{
	public class GameMove
	{
		public Guid Id { get; set; }
		public Enums.PlayerType Owner { get; set; }
		public BoardPosition Position { get; set; }

		public static GameMove CreateFrom(Enums.PlayerType owner, BoardPosition position)
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