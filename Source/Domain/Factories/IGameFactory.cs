using TTT.Domain.Entities;

namespace TTT.Domain.Factories
{
	public interface IGameFactory
	{
		Game CreateNew();
		GameMove CreateFrom(Enums.PlayerType owner, BoardPosition position);
	}
}