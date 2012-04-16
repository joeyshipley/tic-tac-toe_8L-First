using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Providers;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Providers.WinningPositionProviderTests
{
	[Subject("Domain, Providers, WinningPositionProvider")]
	public class When_asking_for_the_possible_winning_moves_and_there_are_none
		: BaseIntegrationTest<AvailableWinningPositionsProvider>
	{
		private static IList<BoardPosition> _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().Build();
		};

		Because of = () => _result = ClassUnderTest.GetPotentialWinningMovesFor(_game, Enums.PlayerType.Human);

		It should_return_an_empty_list_of_positions = () =>
			_result.Count().ShouldEqual(0);
	}

	[Subject("Domain, Providers, WinningPositionProvider")]
	public class When_asking_for_the_possible_winning_moves_and_there_are_none_because_they_have_been_blocked_by_the_other_player
		: BaseIntegrationTest<AvailableWinningPositionsProvider>
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

		Because of = () => _result = ClassUnderTest.GetPotentialWinningMovesFor(_game, Enums.PlayerType.Human);

		It should_return_an_empty_list_of_positions = () =>
			_result.Count().ShouldEqual(0);
	}

	[Subject("Domain, Providers, WinningPositionProvider")]
	public class When_asking_for_the_possible_winning_moves_and_there_is_one
		: BaseIntegrationTest<AvailableWinningPositionsProvider>
	{
		private static IList<BoardPosition> _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().BuildWithPossibleWinningMove();
		};

		Because of = () => _result = ClassUnderTest.GetPotentialWinningMovesFor(_game, Enums.PlayerType.Human);

		It should_return_the_list_of_positions = () =>
			_result.Count().ShouldEqual(1);
	}

	[Subject("Domain, Providers, WinningPositionProvider")]
	public class When_asking_for_the_possible_winning_moves_and_there_is_more_than_one
		: BaseIntegrationTest<AvailableWinningPositionsProvider>
	{
		private static IList<BoardPosition> _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().BuildWithMultiplePossibleWinningMove();
		};

		Because of = () => _result = ClassUnderTest.GetPotentialWinningMovesFor(_game, Enums.PlayerType.Human);

		It should_return_the_list_of_positions = () =>
			_result.Count().ShouldEqual(2);
	}
}