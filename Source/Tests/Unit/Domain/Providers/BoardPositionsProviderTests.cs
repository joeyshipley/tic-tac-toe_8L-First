using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.Providers;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Providers.BoardPositionProviderTests
{
	[Subject("Domain, Providers, GetPotentialWinningMovesFor")]
	public class When_asking_for_the_possible_winning_moves_and_there_are_none
		: BaseIntegrationTest<BoardPositionsProvider>
	{
		private static IList<BoardPosition> _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().Build();
		};

		Because of = () => _result = ClassUnderTest.GetPotentialWinningMovesFor(_game.Moves, Enums.PlayerType.Human);

		It should_return_an_empty_list_of_positions = () =>
			_result.Count().ShouldEqual(0);
	}

	[Subject("Domain, Providers, GetPotentialWinningMovesFor")]
	public class When_asking_for_the_possible_winning_moves_and_there_are_none_because_they_have_been_blocked_by_the_other_player
		: BaseIntegrationTest<BoardPositionsProvider>
	{
		private static IList<BoardPosition> _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 1) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 2) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("A", 3) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 3) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.GetPotentialWinningMovesFor(_game.Moves, Enums.PlayerType.Human);

		It should_return_an_empty_list_of_positions = () =>
			_result.Count().ShouldEqual(0);
	}

	[Subject("Domain, Providers, GetPotentialWinningMovesFor")]
	public class When_asking_for_the_possible_winning_moves_and_there_is_one
		: BaseIntegrationTest<BoardPositionsProvider>
	{
		private static IList<BoardPosition> _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().BuildWithPossibleWinningMove();
		};

		Because of = () => _result = ClassUnderTest.GetPotentialWinningMovesFor(_game.Moves, Enums.PlayerType.Human);

		It should_return_the_list_of_positions = () =>
			_result.Count().ShouldEqual(1);
	}

	[Subject("Domain, Providers, GetPotentialWinningMovesFor")]
	public class When_asking_for_the_possible_winning_moves_and_there_is_more_than_one
		: BaseIntegrationTest<BoardPositionsProvider>
	{
		private static IList<BoardPosition> _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().BuildWithMultiplePossibleWinningMove();
		};

		Because of = () => _result = ClassUnderTest.GetPotentialWinningMovesFor(_game.Moves, Enums.PlayerType.Human);

		It should_return_the_list_of_positions = () =>
			_result.Count().ShouldEqual(2);
	}

	[Subject("Domain, Providers, GetRemainingAvailableBoardPositions")]
	public class When_checking_to_see_what_moves_are_still_available
		: BaseIntegrationTest<BoardPositionsProvider>
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