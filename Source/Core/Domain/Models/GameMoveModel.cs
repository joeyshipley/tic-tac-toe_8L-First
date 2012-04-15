namespace TTT.Core.Domain.Models
{
	public class GameMoveModel
	{
		public Enums.PlayerType Owner { get; set; }
		public Enums.BoardPosition Position { get; set; }
	}
}