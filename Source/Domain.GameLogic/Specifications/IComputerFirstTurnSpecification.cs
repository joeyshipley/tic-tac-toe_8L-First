using TTT.Domain.Entities;

namespace TTT.Domain.GameLogic.Specifications
{
	public interface IComputerFirstTurnSpecification
	{
		bool IsFirstTurnForComputer(Game game);
	}
}