using System.Collections.Generic;
using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Providers
{
	public interface IComputerFirstTurnMoveProvider
	{
		GameMove GetCenterSquareMove();
		IList<GameMove> GetCornerMoves();
	}
}