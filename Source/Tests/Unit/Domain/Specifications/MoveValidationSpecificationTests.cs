using System.Collections.Generic;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Specifications;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Specifications.MoveValidationSpecificationTests
{
	[Subject("Domain, Specifications, MoveValidationSpecification")]
	public class When_determining_if_a_valid_move_is_legitimate_or_not
		: BaseIsolationTest<MoveValidationSpecification>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().Build();
		};

		Because of = () => _result = ClassUnderTest.IsMoveValid(_game, Enums.PlayerType.Human, BoardPosition.CreateFrom("B", 2));

		It should_let_us_know_that_it_is_valid = () =>
			_result.ShouldBeTrue();
	}

	[Subject("Domain, Specifications, MoveValidationSpecification")]
	public class When_determining_if_an_invalid_move_is_legitimate_or_not
		: BaseIsolationTest<MoveValidationSpecification>
	{
		private static bool _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Position = BoardPosition.CreateFrom("B", 2) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.IsMoveValid(_game, Enums.PlayerType.Human, BoardPosition.CreateFrom("B", 2));

		It should_let_us_know_that_it_is_not_valid = () =>
			_result.ShouldBeFalse();
	}
}