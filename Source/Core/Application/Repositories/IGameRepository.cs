using System;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Application.Repositories
{
	public interface IGameRepository
	{
		Game Get(Guid id);
		void Save(Game game);
		void Remove(Game game);
	}
}