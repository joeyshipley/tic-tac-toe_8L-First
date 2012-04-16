using Machine.Specifications;
using TTT.Core.Domain.Processes;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Processes.GameAlgorithmsTests
{
	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_players_first_move_is_not_on_the_middle_center_position
		: BaseIsolationTest<GameAlgorithms>
	{
		Establish context;

		Because of;

		It should_decide_to_return_a_move_for_the_middle_center_position;
	}

	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_players_first_move_is_on_the_middle_center_position
		: BaseIsolationTest<GameAlgorithms>
	{
		Establish context;

		Because of;

		It should_decide_to_return_a_move_for_any_of_the_corner_positions;
	}
}