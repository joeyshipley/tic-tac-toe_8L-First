using TTT.Core.Application.Request;
using TTT.Core.Domain.Models;

namespace TTT.Core.Application.Services
{
	public interface IGameService
	{
		GameModel New();
		GameModel PerformMove(PerformMoveRequest request);
	}
}