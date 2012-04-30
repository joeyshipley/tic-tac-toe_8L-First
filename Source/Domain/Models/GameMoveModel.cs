namespace TTT.Domain.Models
{
	public class GameMoveModel
	{
		public string Owner { get; set; }
		public BoardPositionModel Position { get; set; }
	}
}