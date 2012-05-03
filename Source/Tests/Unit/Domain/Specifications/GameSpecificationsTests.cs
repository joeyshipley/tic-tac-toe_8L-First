using System.Collections.Generic;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Specifications;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Specifications.GameSpecificationsTests
{
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 2) },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("A", 3) },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 1) },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 3) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 2) },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("C", 3) },
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 3) }
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 3) }
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 3) }
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 1) }
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 2) }
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 3) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 3) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 3) }
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 3) }
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
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 1) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 3) }
				})
				.Build();
		};

		Because of = () => _result = ClassUnderTest.IsPlayerWinner(_game);

		It should_let_us_know_the_player_won = () =>
			_result.ShouldBeTrue();
	}
}