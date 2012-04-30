using Machine.Specifications;
using TTT.Application.Request;
using TTT.Application.Services;
using TTT.Application.Models;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Integration.Application.Services
{
	[Subject("Application, Services, GameService")]
	public class When_following_particular_move_list_that_was_resulting_in_a_bug
		: BaseIntegrationTest<GameService>
	{
		private static GameModel _result;

		Establish context;

		Because of = () => 
		{
			// new game setup
			var newGameModel = ClassUnderTest.New();

			// first move to the top left
			ClassUnderTest.PerformMove(new PerformMoveRequest { GameId = newGameModel.GameId, SelectedColumn = "C", SelectedRow = 1 });
	
			// second move to the top center, results in a game over error.
			_result = ClassUnderTest.PerformMove(new PerformMoveRequest { GameId = newGameModel.GameId, SelectedColumn = "B", SelectedRow = 1 });
		};

		It should_not_return_game_over = () => 
			_result.IsGameOver.ShouldBeFalse();
	}
}