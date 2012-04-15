using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Factories
{
	public interface IGameFactory
	{
		Game CreateNew();
		GameMove CreateFrom(Enums.PlayerType owner, Enums.BoardPosition position);
	}
}