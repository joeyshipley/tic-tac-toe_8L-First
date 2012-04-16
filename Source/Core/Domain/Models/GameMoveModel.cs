namespace TTT.Core.Domain.Models
{
	public class GameMoveModel
	{
		public Enums.PlayerType Owner { get; set; }
		public BoardPositionModel Position { get; set; }
	}
}