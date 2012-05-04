using System.Linq;
using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Specifications
{
	public class ComputerFirstTurnSpecification : IComputerFirstTurnSpecification
	{
		public bool IsFirstTurnForComputer(Game game)
		{
			return game.Moves.All(m => m.Owner != Enums.PlayerType.Computer);
		}
	}
}