using System.Linq;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Specifications
{
	public class GameSpecifications : IGameSpecifications
	{
		public bool IsMoveLegitimate(Game game, Enums.PlayerType owner, Enums.BoardPosition position)
		{
			return game.Moves.All(m => m.Position != position);
		}

		public bool IsGameOver(Game game)
		{
			throw new System.NotImplementedException();
		}
	}
}