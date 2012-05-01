using TTT.Domain.Entities;
using TTT.Domain.Helpers;

namespace TTT.Domain.Factories
{
	public interface IGameFactory
	{
		Game CreateNew(RepositoryDelegates.SaveGame saveGame);
		GameMove CreateFrom(Enums.PlayerType owner, BoardPosition position);
	}
}