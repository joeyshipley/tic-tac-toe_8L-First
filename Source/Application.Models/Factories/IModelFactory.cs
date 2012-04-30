using System.Collections.Generic;
using TTT.Domain.Entities;

namespace TTT.Application.Models.Factories
{
	public interface IModelFactory
	{
		GameModel CreateFrom(Game game);
		GameModel CreateFrom(Game game, IList<ValidationError> moveWarnings);
	}
}