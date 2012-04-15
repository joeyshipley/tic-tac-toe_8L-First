using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TTT.Core.Application.Factories;
using TTT.Core.Application.Repositories;
using TTT.Core.Application.Request;
using TTT.Core.Application.Services;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Factories;
using TTT.Core.Domain.Models;
using TTT.Core.Domain.Processes;
using TTT.Core.Domain.Specifications;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Core.Application.Services.GameServiceTests
{
	[Subject("Application, Services, GameService, New game")]
	public class When_loading_a_new_game_up_for_a_player
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;

		Establish context = () =>
		{
			var game = new GameBuilder().Build();
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateNew())
				.Returns(game);
			var gameModel = new GameModelBuilder().BuildNewGame();
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(Moq.It.IsAny<Game>()))
				.Returns(gameModel);
		};

		Because of = () => _result = ClassUnderTest.New();

		It should_create_the_game_in_storage = () => 
			Mocks.GetMock<IGameRepository>()
				.Verify(r => r.Save(Moq.It.IsAny<Game>()));

		It should_return_the_current_games_id = () =>
			_result.GameId.ShouldNotEqual(Guid.Empty);

		It should_return_an_empty_set_of_moves_performed = () =>
			_result.GameMoves.Any().ShouldBeFalse();
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_a_player_is_performing_an_valid_move
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;
		private static Guid _gameId;
		private static Game _game;

		Establish context = () =>
		{
			_gameId = Guid.NewGuid();
			_request = new PerformMoveRequest
			{
				GameId = _gameId,
				Owner = Enums.PlayerType.Human,
				Position = Enums.BoardPosition.MiddleCenter
			};

			_game = new GameBuilder().WithId(_gameId).Build();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(_game);
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsMoveLegitimate(_game, Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<Enums.BoardPosition>()))
				.Returns(true);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(_request.Owner, _request.Position))
				.Returns(new GameMoveBuilder().Build(_request.Owner, _request.Position));
			Mocks.GetMock<IGameAlgorithms>()
				.Setup(ga => ga.DetermineNextMove(_game))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Computer, Enums.BoardPosition.TopLeft));
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsGameOver(_game))
				.Returns(false);
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(_game))
				.Returns(new GameModelBuilder().WithMoves(new List<GameMoveModel>
				{
					new GameMoveModel { Owner = _request.Owner, Position = _request.Position }
				}).Build());
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_validate_the_legitimacy_of_the_move = () =>
			Mocks.GetMock<IGameSpecifications>()
				.Verify(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<Enums.BoardPosition>()));

		It should_apply_the_players_move = () =>
			_game.Moves.Any(g => g.Owner == _request.Owner 
				&& g.Position == _request.Position)
				.ShouldBeTrue();

		It should_calculate_the_computers_next_move = () =>
			Mocks.GetMock<IGameAlgorithms>()
				.Verify(ga => ga.DetermineNextMove(Moq.It.IsAny<Game>()));
	
		It should_determine_if_the_game_should_continue = () =>
			Mocks.GetMock<IGameSpecifications>()
				.Verify(s => s.IsGameOver(Moq.It.IsAny<Game>()));

		It should_return_the_current_set_of_moves_performed = () =>
			_result.GameMoves.Any(g => g.Owner == _request.Owner 
				&& g.Position == _request.Position)
			.ShouldBeTrue();
	}
	
	[Subject("Application, Services, GameService, Existing game")]
	public class When_a_player_is_performing_an_valid_move_and_the_game_does_not_end
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;
		private static Guid _gameId;
		private static Game _game;

		Establish context = () =>
		{
			_gameId = Guid.NewGuid();
			_request = new PerformMoveRequest
			{
				GameId = _gameId,
				Owner = Enums.PlayerType.Human,
				Position = Enums.BoardPosition.MiddleCenter
			};

			_game = new GameBuilder().WithId(_gameId).Build();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(_game);
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsMoveLegitimate(_game, Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<Enums.BoardPosition>()))
				.Returns(true);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(_request.Owner, _request.Position))
				.Returns(new GameMoveBuilder().Build(_request.Owner, _request.Position));
			Mocks.GetMock<IGameAlgorithms>()
				.Setup(ga => ga.DetermineNextMove(_game))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Computer, Enums.BoardPosition.TopLeft));
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsGameOver(_game))
				.Returns(false);
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(_game))
				.Returns(new GameModelBuilder().WithMoves(new List<GameMoveModel>
				{
					new GameMoveModel { Owner = _request.Owner, Position = _request.Position }
				}).Build());
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_return_whether_or_not_the_game_should_go_on = () =>
			_result.IsGameOver.ShouldBeFalse();
	}
	
	[Subject("Application, Services, GameService, Existing game")]
	public class When_a_player_is_performing_an_valid_move_and_the_game_comes_to_an_end
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;
		private static Guid _gameId;
		private static Game _game;

		Establish context = () =>
		{
			_gameId = Guid.NewGuid();
			_request = new PerformMoveRequest
			{
				GameId = _gameId,
				Owner = Enums.PlayerType.Human,
				Position = Enums.BoardPosition.BottomLeft
			};

			_game = new GameBuilder().WithId(_gameId).BuildGameWithValidMoves();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(_game);
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsMoveLegitimate(_game, Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<Enums.BoardPosition>()))
				.Returns(true);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(_request.Owner, _request.Position))
				.Returns(new GameMoveBuilder().Build(_request.Owner, _request.Position));
			Mocks.GetMock<IGameAlgorithms>()
				.Setup(ga => ga.DetermineNextMove(_game))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Computer, Enums.BoardPosition.TopLeft));
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsGameOver(_game))
				.Returns(true);
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(_game))
				.Returns(new GameModelBuilder().WithMoves(new List<GameMoveModel>
				{
					new GameMoveModel { Owner = _request.Owner, Position = _request.Position }
				})
				.WithIsGameOver(true)
				.Build());
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_return_whether_or_not_the_game_should_go_on = () =>
			_result.IsGameOver.ShouldBeTrue();
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_a_player_is_performing_an_invalid_move
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;
		private static Guid _gameId;
		private static Game _game;

		Establish context = () =>
		{
			_gameId = Guid.NewGuid();

			var moves = new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = Enums.BoardPosition.TopLeft },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = Enums.BoardPosition.MiddleCenter }
			};
			var game = new GameBuilder().WithId(_gameId).WithMoves(moves).Build();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(game);
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<Enums.BoardPosition>()))
				.Returns(false);

			_request = new PerformMoveRequest
			{
				GameId = _gameId,
				Owner = Enums.PlayerType.Human,
				Position = Enums.BoardPosition.MiddleCenter
			};

		};

		Because of;

		It should_invalidate_the_legitimacy_of_the_move;

		It should_not_apply_the_players_move;

		It should_not_perform_the_computers_next_move;

		It should_not_determine_the_outcome_of_the_game;

		It should_return_the_warning_messages;
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_the_game_does_not_end_in_a_draw
		: BaseIsolationTest<GameService>
	{
		Establish context;

		Because of;

		It should_inform_the_player_of_the_developers_failure_to_create_an_unbeatable_tic_tac_toe_game;
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_the_game_ends_in_a_draw
		: BaseIsolationTest<GameService>
	{
		Establish context;

		Because of;

		It should_inform_the_player_that_the_game_is_infact_unbeatable_and_that_continuing_will_only_make_them_feel_worse;
	}
}