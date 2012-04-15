using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Specifications
{
	public class GameSpecifications : IGameSpecifications
	{
		public bool IsMoveLegitimate(Game game, Enums.PlayerType owner, Enums.BoardPosition position)
		{
			throw new System.NotImplementedException();
		}

		public bool IsGameOver(Game game)
		{
			throw new System.NotImplementedException();
		}
	}
}