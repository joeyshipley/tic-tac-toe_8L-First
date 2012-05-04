using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Providers;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Providers.BoardPositionProviderTests
{
	[Subject("Domain, Providers, GetRemainingAvailableBoardPositions")]
	public class When_checking_to_see_what_moves_are_still_available
		: BaseIntegrationTest<AvailableBoardPositionsProvider>
	{
		private static IList<BoardPosition> _result;
		private static Game _game;
		private static int _numberOfSelectedMoves;
		private static int _numberOfMaxAvailableMoves;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Position = BoardPosition.CreateFrom("B", 2) }
			})
			.Build();
			_numberOfSelectedMoves = _game.Moves.Count();
			_numberOfMaxAvailableMoves = Constants.BoardPositionRowLength * Constants.BoardPositionRowLength;
		};

		Because of = () => _result = ClassUnderTest.GetRemainingAvailableBoardPositions(_game);

		It should_return_all_non_selected_board_positions = () =>
			_result.Count().ShouldEqual(_numberOfMaxAvailableMoves - _numberOfSelectedMoves);
	}
}