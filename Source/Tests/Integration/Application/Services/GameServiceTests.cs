using System;
using System.Collections.Generic;
using Machine.Specifications;
using StructureMap;
using TTT.Application.Repositories;
using TTT.Application.Request;
using TTT.Application.Services;
using TTT.Application.Models;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Integration.Application.Services
{
	[Subject("Application, Services, GameService, BugFix")]
	public class When_following_particular_move_list_that_was_resulting_in_a_bug
		: BaseIntegrationTest<GameService>
	{
		private static GameModel _result;

		Establish context;

		Because of = () => 
		{
			// new game setup
			var newGameModel = ClassUnderTest.New();

			// first move to the top left
			ClassUnderTest.PerformMove(new PerformMoveRequest { GameId = newGameModel.GameId, SelectedColumn = "C", SelectedRow = 1 });
	
			// second move to the top center, results in a game over error.
			_result = ClassUnderTest.PerformMove(new PerformMoveRequest { GameId = newGameModel.GameId, SelectedColumn = "B", SelectedRow = 1 });
		};

		It should_not_return_game_over = () => 
			_result.IsGameOver.ShouldBeFalse();
	}

	[Subject("Application, Services, GameService, BugFix")]
	public class When_following_a_move_set_where_you_start_in_the_center_and_play_the_opponents_opposite_corner
		: BaseIntegrationTest<GameService>
	{
		private static GameModel _result;
		private static Guid _gameId;

		Establish context = () => 
		{
			var newGameModel = ClassUnderTest.New();
			_gameId = newGameModel.GameId;
			var game = new GameBuilder()
				.WithId(_gameId)
				.WithMoves(new List<GameMove>
				{
					new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("B", 2)),
					new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("C", 1)),
					new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 3)),
					new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("A", 1)),
					new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("B", 1)),
					new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("B", 3))
				})
				.Build();
			var gameRepository = ObjectFactory.GetInstance<IGameRepository>();
			gameRepository.Save(game);
		};

		Because of = () => _result = ClassUnderTest.PerformMove(new PerformMoveRequest { GameId = _gameId, SelectedColumn = "C", SelectedRow = 3 });

		It should_not_return_game_over = () => 
			_result.IsGameOver.ShouldBeFalse();
	}
}