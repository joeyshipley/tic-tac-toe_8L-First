using Machine.Specifications;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Specifications;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Specifications.GameSpecificationsTests
{
	[Subject("Domain, Specifications, GameSpecifications")]
	public class When_determining_if_a_valid_move_is_legitimate_or_not
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context;

		Because of = () => _result = ClassUnderTest.IsMoveLegitimate(_game, Enums.PlayerType.Human, Enums.BoardPosition.MiddleCenter);

		It should_let_us_know_that_it_is_valid;
	}

	[Subject("Domain, Specifications, GameSpecifications")]
	public class When_determining_if_an_invalid_move_is_legitimate_or_not
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context;

		Because of = () => _result = ClassUnderTest.IsMoveLegitimate(_game, Enums.PlayerType.Human, Enums.BoardPosition.MiddleCenter);

		It should_let_us_know_that_it_is_not_valid;
	}

	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_game_that_should_go_on_is_over_or_not
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context;

		Because of = () => _result = ClassUnderTest.IsGameOver(_game);

		It should_let_us_know_it_is_not_over;
	}

	[Subject("Domain, Specifications, GameAlgorithms")]
	public class When_determining_a_game_that_should_not_go_on_is_over_or_not
		: BaseIsolationTest<GameSpecifications>
	{
		private static bool _result;
		private static Game _game;

		Establish context;

		Because of = () => _result = ClassUnderTest.IsGameOver(_game);

		It should_let_us_know_it_is_in_fact_over;
	}
}