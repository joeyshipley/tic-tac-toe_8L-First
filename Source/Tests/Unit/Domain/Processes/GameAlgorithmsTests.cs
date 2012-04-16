using System.Collections.Generic;
using Machine.Specifications;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Processes;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Processes.GameAlgorithmsTests
{
	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_players_first_move_is_not_on_the_middle_center_position
		: BaseIsolationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.DetermineNextMove(_game);

		It should_decide_to_return_a_move_for_the_middle_center_position = () =>
			_result.Position.Equals(BoardPosition.CreateFrom("B", 2));
	}

	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_players_first_move_is_on_the_middle_center_position
		: BaseIsolationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 2) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.DetermineNextMove(_game);

		It should_decide_to_return_a_move_for_any_of_the_corner_positions = () =>
		{
			var isCornerPosition = _result.Position.Equals(BoardPosition.CreateFrom("A", 1))
				|| _result.Position.Equals(BoardPosition.CreateFrom("A", 3))
				|| _result.Position.Equals(BoardPosition.CreateFrom("C", 1))
				|| _result.Position.Equals(BoardPosition.CreateFrom("C", 3));
			isCornerPosition.ShouldBeTrue();
		};
	}
}