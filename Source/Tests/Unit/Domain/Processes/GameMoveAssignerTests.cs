using System.Linq;
using Machine.Specifications;
using TTT.Application.Repositories;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.Factories;
using TTT.Domain.GameLogic.Processes;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Processes.GameAlgorithmsTests
{
	[Subject("Domain, Processes, GameMoveAssigner")]
	public class When_asking_the_move_assigner_to_apply_a_move_to_the_game
		: BaseIsolationTest<GameMoveAssigner>
	{
		private static GameMove _result;
		private static Game _game;
		private static BoardPosition _boardPosition;
		private static IGameRepository _repository;

		Establish context = () => 
		{
			_game = new GameBuilder().Build();
			_boardPosition = BoardPosition.CreateFrom("A", 1);
			_repository = Mocks.GetMock<IGameRepository>().Object;

			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Human, _boardPosition));
		};

		Because of = () => ClassUnderTest.AssignMove(_game, Enums.PlayerType.Human, _boardPosition, _repository.Save);

		It should_add_the_move_to_the_game = () =>
			_game.Moves.Any(m => m.Position.Equals(_boardPosition)).ShouldBeTrue();

		It should_apply_the_players_move = () =>
			_game.Moves.Any(g => g.Owner == Enums.PlayerType.Human
				&& g.Position.Equals(_boardPosition))
				.ShouldBeTrue();
	}
}