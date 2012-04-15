using Machine.Specifications;
using TTT.Core.Application.Services;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Core.Application.Services.GameServiceTests
{
	[Subject("Application, Services, GameService, New game")]
	public class When_loading_a_new_game_up_for_a_player
		: BaseIsolationTest<GameService>
	{
		Establish context;

		Because of;

		It should_create_the_game_in_storage;

		It should_return_the_current_games_id;

		It should_return_an_empty_set_of_moves_performed;
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