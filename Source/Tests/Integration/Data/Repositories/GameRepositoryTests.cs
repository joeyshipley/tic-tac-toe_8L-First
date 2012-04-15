using Machine.Specifications;
using TTT.Core.Data.Repositories;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Integration.Data.Repositories
{
	[Subject("Data, Repositories, Game")]
	public class When_asking_for_a_game
		: BaseIntegrationTest<GameRepository>
	{
		Establish context;

		Because of;

		It should_be_able_to_return_an_existing_game;
	}

	[Subject("Data, Repositories, Game")]
	public class When_saving_a_game
		: BaseIntegrationTest<GameRepository>
	{
		Establish context;

		Because of;

		It should_be_able_to_store_a_game;
	}

	[Subject("Data, Repositories, Game")]
	public class When_removing_a_game
		: BaseIntegrationTest<GameRepository>
	{
		Establish context;

		Because of;

		It should_be_able_remove_a_game_from_the_storage;
	}
}