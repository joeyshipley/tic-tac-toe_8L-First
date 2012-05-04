using System.Linq;
using System.Collections.Generic;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Providers;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Providers
{
	[Subject("Domain, Providers, ComputerFirstTurnMoveProvider, GetCenterSquareMove")]
	public class When_asking_for_the_center_square_move
		: BaseIntegrationTest<ComputerFirstTurnMoveProvider>
	{
		private static GameMove _result;

		Establish context;

		Because of = () => _result = ClassUnderTest.GetCenterSquareMove();

		It should_return_the_center_square_move = () =>
			(_result.Position.Column == "B" && _result.Position.Row == 2).ShouldBeTrue();
	}

	[Subject("Domain, Providers, ComputerFirstTurnMoveProvider, GetCornerMoves")]
	public class When_asking_for_the_corner_squares_moves
		: BaseIntegrationTest<ComputerFirstTurnMoveProvider>
	{
		private static IList<GameMove> _result;

		Establish context;

		Because of = () => _result = ClassUnderTest.GetCornerMoves();

		It should_return_the_top_left_corner = () =>
			_result.Any(m => m.Position.Column == "A" && m.Position.Row == 1).ShouldBeTrue();
		
		It should_return_the_top_right_corner = () =>
			_result.Any(m => m.Position.Column == "A" && m.Position.Row == 3).ShouldBeTrue();
	
		It should_return_the_bottom_left_corner = () =>
			_result.Any(m => m.Position.Column == "C" && m.Position.Row == 1).ShouldBeTrue();

		It should_return_the_bottom_right_corner = () =>
			_result.Any(m => m.Position.Column == "C" && m.Position.Row == 3).ShouldBeTrue();
	}
}