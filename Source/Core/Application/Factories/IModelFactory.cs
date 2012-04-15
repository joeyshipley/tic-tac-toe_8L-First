using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Models;

namespace TTT.Core.Application.Factories
{
	public interface IModelFactory
	{
		GameModel CreateFrom(Game game);
	}
}