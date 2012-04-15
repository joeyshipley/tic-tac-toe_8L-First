using Machine.Specifications;
using TTT.Core.Application.Factories;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Core.Application.Factories.ModelFactoryTests
{
	[Subject("Application, Factories, ModelFactory, GameModel creation")]
	public class When_asking_for_a_game_model
		: BaseIsolationTest<ModelFactory>
	{
		Establish context;

		Because of;

		It should_return_a_game_model;

		It should_set_the_models_id_to_match_the_game;

		It should_build_the_moves_from_the_games_moves;
	}
}