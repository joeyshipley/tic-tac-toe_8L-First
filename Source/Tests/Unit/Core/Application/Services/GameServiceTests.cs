using System;
using System.Linq;
using Machine.Specifications;
using TTT.Core.Application.Factories;
using TTT.Core.Application.Repositories;
using TTT.Core.Application.Services;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Factories;
using TTT.Core.Domain.Models;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Core.Application.Services.GameServiceTests
{
	[Subject("Application, Services, GameService, New game")]
	public class When_loading_a_new_game_up_for_a_player
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;

		Establish context = () =>
		{
			var game = new GameBuilder().Build();
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateNew())
				.Returns(game);
			var gameModel = new GameModelBuilder().BuildNewGame();
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(Moq.It.IsAny<Game>()))
				.Returns(gameModel);
		};

		Because of = () => _result = ClassUnderTest.New();

		It should_create_the_game_in_storage = () => 
			Mocks.GetMock<IGameRepository>()
				.Verify(r => r.Save(Moq.It.IsAny<Game>()));

		It should_return_the_current_games_id = () =>
			_result.GameId.ShouldNotEqual(Guid.Empty);

		It should_return_an_empty_set_of_moves_performed = () =>
			_result.GameMoves.Any().ShouldBeFalse();
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_a_player_is_performing_an_invalid_move
		: BaseIsolationTest<GameService>
	{
		Establish context;

		Because of;

		It should_invalidate_the_legitimacy_of_the_move;

		It should_not_apply_the_players_move;

		It should_not_perform_the_computers_next_move;

		It should_not_determine_the_outcome_of_the_game;

		It should_return_the_warning_messages;
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_a_player_is_performing_an_valid_move
		: BaseIsolationTest<GameService>
	{
		Establish context;

		Because of;

		It should_validate_the_legitimacy_of_the_move;

		It should_apply_the_players_move;

		It should_calculate_the_computers_next_move;
	
		It should_determine_if_the_game_should_continue;

		It should_return_the_current_set_of_moves_performed;
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_the_game_does_not_end_in_a_draw
		: BaseIsolationTest<GameService>
	{
		Establish context;

		Because of;

		It should_inform_the_player_of_the_developers_failure_to_create_an_unbeatable_tic_tac_toe_game;
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_the_game_ends_in_a_draw
		: BaseIsolationTest<GameService>
	{
		Establish context;

		Because of;

		It should_inform_the_player_that_the_game_is_infact_unbeatable_and_that_continuing_will_only_make_them_feel_worse;
	}
}