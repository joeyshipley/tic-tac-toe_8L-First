using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Specifications;
using TTT.Domain.GameLogic.Validators;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Validators
{
	[Subject("Domain, Validators, MoveValidator")]
	public class When_validating_a_move_that_is_valid
		: BaseIsolationTest<MoveValidator>
	{
		private static IList<ValidationError> _result;
		private static Game _game;
		private static BoardPosition _boardPosition;

		Establish context = () => 
		{
			_game = new GameBuilder().Build();
			_boardPosition = BoardPosition.CreateFrom("A", 1);

			Mocks.GetMock<IMoveValidationSpecification>()
				.Setup(p => p.IsMoveValid(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(true);
		};

		Because of = () => _result = ClassUnderTest.ValidateMove(_game, Enums.PlayerType.Human, _boardPosition);

		It should_not_return_validation_messages = () =>
			_result.Any().ShouldBeFalse();
	}

	[Subject("Domain, Validators, MoveValidator")]
	public class When_validating_a_move_that_is_not_valid
		: BaseIsolationTest<MoveValidator>
	{
		private static IList<ValidationError> _result;
		private static Game _game;
		private static BoardPosition _boardPosition;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 1))
			}).Build();
			_boardPosition = BoardPosition.CreateFrom("A", 1);

			Mocks.GetMock<IMoveValidationSpecification>()
				.Setup(p => p.IsMoveValid(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(false);
		};

		Because of = () => _result = ClassUnderTest.ValidateMove(_game, Enums.PlayerType.Human, _boardPosition);

		It should_return_validation_messages = () =>
			_result.Any().ShouldBeTrue();
	}
}