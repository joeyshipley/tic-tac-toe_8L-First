using System.Collections.Generic;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Specifications;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Specifications.ComputerFirstTurnSpecificationTests
{
	[Subject("Domain, Specifications, ComputerFirstTurnSpecification, IsFirstTurnForComputer")]
	public class When_asking_if_its_the_computers_first_turn_and_it_is
		: BaseIsolationTest<ComputerFirstTurnSpecification>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 2) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.IsFirstTurnForComputer(_game);

		It should_let_us_know_that_it_is_the_computers_first_turn = () =>
			_result.ShouldBeTrue();
	}

	[Subject("Domain, Specifications, ComputerFirstTurnSpecification, IsFirstTurnForComputer")]
	public class When_determining_if_an_invalid_move_is_legitimate_or_not
		: BaseIsolationTest<ComputerFirstTurnSpecification>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 3) },
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.IsFirstTurnForComputer(_game);

		It should_let_us_know_that_it_is_not_the_computers_first_turn = () =>
			_result.ShouldBeFalse();
	}
}