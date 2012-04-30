using System.Collections.Generic;
using TTT.Domain.Entities;
using TTT.Domain.Models;

namespace TTT.Application.Factories
{
	public interface IModelFactory
	{
		GameModel CreateFrom(Game game);
		GameModel CreateFrom(Game game, IList<ValidationError> moveWarnings);
	}
}