using System.Collections.Generic;
using Machine.Specifications;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Specifications;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Specifications.GameSpecificationsTests
{
	[Subject("Domain, Specifications, GameSpecifications")]
	public class When_determining_if_a_valid_move_is_legitimate_or_not
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().Build();
		};

		Because of = () => _result = ClassUnderTest.IsMoveLegitimate(_game, Enums.PlayerType.Human, Enums.BoardPosition.MiddleCenter);

		It should_let_us_know_that_it_is_valid = () =>
			_result.ShouldBeTrue();
	}

	[Subject("Domain, Specifications, GameSpecifications")]
	public class When_determining_if_an_invalid_move_is_legitimate_or_not
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Position = Enums.BoardPosition.MiddleCenter }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.IsMoveLegitimate(_game, Enums.PlayerType.Human, Enums.BoardPosition.MiddleCenter);

		It should_let_us_know_that_it_is_not_valid = () =>
			_result.ShouldBeFalse();
	}

	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_if_a_game_with_no_sets_completed_or_still_has_moves_available_is_over
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () =>
		{
			_game = new GameBuilder().Build();
		};

		Because of = () => _result = ClassUnderTest.IsGameOver(_game);

		It should_let_us_know_it_is_not_over = () =>
			_result.ShouldBeFalse();
	}
	

	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_game_that_has_all_of_its_positions_selected
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopCenter },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = Enums.BoardPosition.TopRight },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = Enums.BoardPosition.MiddleLeft },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = Enums.BoardPosition.MiddleCenter },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleRight },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomCenter },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = Enums.BoardPosition.BottomRight },
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsGameOver(_game);

		It should_let_us_know_it_is_in_fact_over = () =>
			_result.ShouldBeTrue();
	}

	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_winner_for_a_game_that_has_a_set_of_three_on_the_top_row
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopCenter },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopRight }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}
	
	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_winner_for_a_game_that_has_a_set_of_three_on_the_middle_row
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleCenter },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleRight }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}

	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_winner_for_a_game_that_has_a_set_of_three_on_the_bottom_row
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomCenter },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomRight }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}
	
	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_winner_for_a_game_that_has_a_set_of_three_on_the_left_row
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomLeft }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}
	
	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_winner_for_a_game_that_has_a_set_of_three_on_the_center_row
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopCenter },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleCenter },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomCenter }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}
	
	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_winner_for_a_game_that_has_a_set_of_three_on_the_right_row
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopRight },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleRight },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomRight }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}
	
	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_winner_for_a_game_that_has_a_set_of_three_on_top_left_diagonal_row
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleCenter },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomRight }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}
	
	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_winner_for_a_game_that_has_a_set_of_three_on_bottom_left_diagonal_row
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(
				new List<GameMove> 
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.BottomLeft },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.MiddleCenter },
					new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopRight }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}
}