using System.Collections.Generic;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Models;

namespace TTT.Core.Application.Factories
{
	public interface IModelFactory
	{
		GameModel CreateFrom(Game game);
		GameModel CreateFrom(Game game, IList<ValidationError> moveWarnings);
	}
}