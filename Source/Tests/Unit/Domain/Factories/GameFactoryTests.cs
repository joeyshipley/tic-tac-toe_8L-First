using Machine.Specifications;
using TTT.Core.Domain.Factories;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Factories.GameFactoryTests
{
	[Subject("Domain, Factories, GameFactory, Game creation")]
	public class When_asking_for_a_game
		: BaseIsolationTest<GameFactory>
	{
		Establish context;

		Because of;

		It should_create_a_game;

		It should_assign_the_game_an_id;

		It should_assing_an_empty_list_of_moves;
	}
}