using TTT.Application.Request;
using TTT.Domain.Models;

namespace TTT.Application.Services
{
	public interface IGameService
	{
		GameModel New();
		GameModel PerformMove(PerformMoveRequest request);
	}
}