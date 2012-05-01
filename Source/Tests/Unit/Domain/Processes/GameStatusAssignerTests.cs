using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TTT.Application.Repositories;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.Factories;
using TTT.Domain.GameLogic.Processes;
using TTT.Domain.GameLogic.Specifications;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Processes.GameAlgorithmsTests
{
	[Subject("Domain, Processes, GameStatusAssigner")]
	public class When_asking_for_the_game_status_to_be_set_for_a_game_that_is_not_yet_over
		: BaseIsolationTest<GameStatusAssigner>
	{
		private static Game _game;
		private static IGameRepository _repository;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 1)),
				new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("A", 2))
			}).Build();
			_repository = Mocks.GetMock<IGameRepository>().Object;
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsGameOver(Moq.It.IsAny<Game>()))
				.Returns(false);
		};

		Because of = () => ClassUnderTest.AssignGameStatus(_game, _repository.Save);

		It should_set_the_is_game_over_property_to_false = () =>
			_game.IsGameOver.ShouldBeFalse();
	}

	[Subject("Domain, Processes, GameStatusAssigner")]
	public class When_asking_for_the_game_status_to_be_set_for_a_game_that_is_over
		: BaseIsolationTest<GameStatusAssigner>
	{
		private static Game _game;
		private static IGameRepository _repository;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 1)),
				new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("C", 1)),
				new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 2)),
				new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("C", 2)),
				new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 3))
			}).Build();
			_repository = Mocks.GetMock<IGameRepository>().Object;
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsGameOver(Moq.It.IsAny<Game>()))
				.Returns(true);
		};

		Because of = () => ClassUnderTest.AssignGameStatus(_game, _repository.Save);

		It should_set_the_is_game_over_property_to_true = () =>
			_game.IsGameOver.ShouldBeTrue();
	}
}