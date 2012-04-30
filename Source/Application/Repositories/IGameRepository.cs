using System;
using TTT.Domain.Entities;

namespace TTT.Application.Repositories
{
	public interface IGameRepository
	{
		Game Get(Guid id);
		void Save(Game game);
		void Remove(Game game);
	}
}