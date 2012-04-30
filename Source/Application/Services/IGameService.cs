using TTT.Application.Models;
using TTT.Application.Request;

namespace TTT.Application.Services
{
	public interface IGameService
	{
		GameModel New();
		GameModel PerformMove(PerformMoveRequest request);
	}
}