using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Moq;
using TTT.Application.Models.Factories;
using TTT.Application.Repositories;
using TTT.Application.Request;
using TTT.Application.Services;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.Factories;
using TTT.Application.Models;
using TTT.Domain.Processes;
using TTT.Domain.Specifications;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;
using It = Machine.Specifications.It;

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
		private static BoardPosition _position;

		Establish context = () =>
		{
			_gameId = Guid.NewGuid();
			_request = new PerformMoveRequest
			{
				GameId = _gameId,
				SelectedColumn = "B",
				SelectedRow = 2
			};

			_game = new GameBuilder().WithId(_gameId).Build();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(_game);
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsMoveLegitimate(_game, Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(true);
			_position = BoardPosition.CreateFrom(_request.SelectedColumn, _request.SelectedRow);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(Enums.PlayerType.Human, _position))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Human, _position));
			Mocks.GetMock<IGameAlgorithms>()
				.Setup(ga => ga.DetermineNextMove(_game))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Computer, _position));
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsGameOver(_game))
				.Returns(false);
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(_game))
				.Returns(new GameModelBuilder().WithMoves(new List<GameMoveModel>
				{
					new GameMoveModel { Owner = Enums.PlayerType.Human.ToString(), Position = BoardPositionModel.CreateFrom(_position) }
				}).Build());
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_validate_the_legitimacy_of_the_move = () =>
			Mocks.GetMock<IGameSpecifications>()
				.Verify(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()));

		It should_apply_the_players_move = () =>
			_game.Moves.Any(g => g.Owner == Enums.PlayerType.Human
				&& g.Position.Equals(_position))
				.ShouldBeTrue();

		It should_calculate_the_computers_next_move = () =>
			Mocks.GetMock<IGameAlgorithms>()
				.Verify(ga => ga.DetermineNextMove(Moq.It.IsAny<Game>()));
	
		It should_determine_if_the_game_should_continue = () =>
			Mocks.GetMock<IGameSpecifications>()
				.Verify(s => s.IsGameOver(Moq.It.IsAny<Game>()));

		It should_return_the_current_set_of_moves_performed = () =>
			_result.GameMoves.Any().ShouldBeTrue();
	}
	
	[Subject("Application, Services, GameService, Existing game")]
	public class When_a_player_is_performing_an_valid_move_and_the_game_does_not_end
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;
		private static Guid _gameId;
		private static Game _game;
		private static BoardPosition _position;

		Establish context = () =>
		{
			_gameId = Guid.NewGuid();
			_request = new PerformMoveRequest
			{
				GameId = _gameId,
				SelectedColumn = "B",
				SelectedRow = 1
			};

			_game = new GameBuilder().WithId(_gameId).Build();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(_game);
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsMoveLegitimate(_game, Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(true);
			_position = BoardPosition.CreateFrom(_request.SelectedColumn, _request.SelectedRow);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(Enums.PlayerType.Human, _position))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Human, _position));
			Mocks.GetMock<IGameAlgorithms>()
				.Setup(ga => ga.DetermineNextMove(_game))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("A", 1)));
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsGameOver(_game))
				.Returns(false);
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(_game))
				.Returns(new GameModelBuilder().WithMoves(new List<GameMoveModel>
				{
					new GameMoveModel { Owner = Enums.PlayerType.Human.ToString(), Position = BoardPositionModel.CreateFrom(_position) }
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
		private static BoardPosition _position;

		Establish context = () =>
		{
			_gameId = Guid.NewGuid();
			_request = new PerformMoveRequest
			{
				GameId = _gameId,
				SelectedColumn = "A",
				SelectedRow = 3
			};

			_game = new GameBuilder().WithId(_gameId).BuildGameWithValidMoves();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(_game);
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsMoveLegitimate(_game, Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(true);
			_position = BoardPosition.CreateFrom(_request.SelectedColumn, _request.SelectedRow);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(Enums.PlayerType.Human, _position))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Human, _position));
			Mocks.GetMock<IGameAlgorithms>()
				.Setup(ga => ga.DetermineNextMove(_game))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("A", 1)));
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsGameOver(_game))
				.Returns(true);
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(_game))
				.Returns(new GameModelBuilder().WithMoves(new List<GameMoveModel>
				{
					new GameMoveModel { Owner = Enums.PlayerType.Human.ToString(), Position = BoardPositionModel.CreateFrom(_position) }
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
		private static BoardPosition _position;

		Establish context = () =>
		{
			_gameId = Guid.NewGuid();
			_request = new PerformMoveRequest
			{
				GameId = _gameId,
				SelectedColumn = "B",
				SelectedRow = 1
			};

			var moves = new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) }
			};
			var game = new GameBuilder().WithId(_gameId).WithMoves(moves).WithIsGameOver(false).Build();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(game);
			Mocks.GetMock<IGameSpecifications>()
				.Setup(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(false);
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(Moq.It.IsAny<Game>(), Moq.It.IsAny<IList<ValidationError>>()))
				.Returns(new GameModel 
				{
					GameId = game.Id, 
					IsGameOver = game.IsGameOver, 
					MoveWarnings = new List<ValidationError> 
					{
						new ValidationError { Type = "InvalidMove" }
					}
				});
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_invalidate_the_legitimacy_of_the_move = () =>
			Mocks.GetMock<IGameSpecifications>()
				.Verify(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()));

		It should_not_end_the_game = () =>
			_result.IsGameOver.ShouldBeFalse();

		It should_return_the_warning_messages = () =>
			_result.MoveWarnings.Any(w => w.Type == "InvalidMove").ShouldBeTrue();
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_the_game_does_not_end_in_a_draw_because_the_player_won
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;

		Establish context = () =>
		{
			_request = new PerformMoveRequest { GameId = Guid.NewGuid(), SelectedColumn = "A", SelectedRow = 1 };
			var game = new GameBuilder().BuildGameWithValidMoves();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(game);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(Enums.PlayerType.Human, Moq.It.IsAny<BoardPosition>()))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 1)));
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(Moq.It.IsAny<Game>()))
				.Returns(new GameModel 
				{
					IsGameOver = true
				});
			var gameSpec = Mocks.GetMock<IGameSpecifications>();
			gameSpec
				.Setup(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(true);
			gameSpec
				.Setup(s => s.IsGameOver(Moq.It.IsAny<Game>()))
				.Returns(true);
			gameSpec
				.Setup(s => s.IsPlayerWinner(Moq.It.IsAny<Game>()))
				.Returns(true);
			gameSpec
				.Setup(s => s.IsComputerWinner(Moq.It.IsAny<Game>()))
				.Returns(false);
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_inform_the_player_of_the_developers_failure_to_create_an_unbeatable_tic_tac_toe_game = () =>
			_result.IsPlayerWinner.ShouldBeTrue();
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_the_game_does_not_end_in_a_draw_because_the_computer_won
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;

		Establish context = () =>
		{
			_request = new PerformMoveRequest { GameId = Guid.NewGuid(), SelectedColumn = "A", SelectedRow = 1 };
			var game = new GameBuilder().BuildGameWithValidMoves();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(game);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(Enums.PlayerType.Human, Moq.It.IsAny<BoardPosition>()))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 1)));
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(Moq.It.IsAny<Game>()))
				.Returns(new GameModel 
				{
					IsGameOver = true
				});
			var gameSpec = Mocks.GetMock<IGameSpecifications>();
			gameSpec
				.Setup(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(true);
			gameSpec
				.Setup(s => s.IsGameOver(Moq.It.IsAny<Game>()))
				.Returns(true);
			gameSpec
				.Setup(s => s.IsPlayerWinner(Moq.It.IsAny<Game>()))
				.Returns(false);
			gameSpec
				.Setup(s => s.IsComputerWinner(Moq.It.IsAny<Game>()))
				.Returns(true);
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_inform_the_player_that_the_game_is_infact_unbeatable_and_that_continuing_will_only_make_them_feel_worse = () =>
			_result.IsComputerWinner.ShouldBeTrue();
	}

	[Subject("Application, Services, GameService, Existing game")]
	public class When_the_game_ends_in_a_draw
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;

		Establish context = () =>
		{
			_request = new PerformMoveRequest { GameId = Guid.NewGuid(), SelectedColumn = "A", SelectedRow = 1 };
			var game = new GameBuilder().BuildGameWithValidMoves();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(game);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(Enums.PlayerType.Human, Moq.It.IsAny<BoardPosition>()))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 1)));
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(Moq.It.IsAny<Game>()))
				.Returns(new GameModel 
				{
					IsGameOver = true
				});
			var gameSpec = Mocks.GetMock<IGameSpecifications>();
			gameSpec
				.Setup(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(true);
			gameSpec
				.Setup(s => s.IsGameOver(Moq.It.IsAny<Game>()))
				.Returns(true);
			gameSpec
				.Setup(s => s.IsPlayerWinner(Moq.It.IsAny<Game>()))
				.Returns(false);
			gameSpec
				.Setup(s => s.IsComputerWinner(Moq.It.IsAny<Game>()))
				.Returns(false);
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_inform_the_player_that_the_game_is_infact_unbeatable_and_that_continuing_will_only_make_them_feel_worse = () =>
			_result.IsGameDraw.ShouldBeTrue();
	}
	
	[Subject("Application, Services, GameService, Existing game")]
	public class When_the_player_makes_the_last_move
		: BaseIsolationTest<GameService>
	{
		private static GameModel _result;
		private static PerformMoveRequest _request;

		Establish context = () =>
		{
			_request = new PerformMoveRequest { GameId = Guid.NewGuid(), SelectedColumn = "A", SelectedRow = 1 };

			var game = new GameBuilder().BuildGameWithValidMoves();
			Mocks.GetMock<IGameRepository>()
				.Setup(f => f.Get(Moq.It.IsAny<Guid>()))
				.Returns(game);
			var gameSpec = Mocks.GetMock<IGameSpecifications>();
			gameSpec
				.Setup(s => s.IsMoveLegitimate(Moq.It.IsAny<Game>(), Moq.It.IsAny<Enums.PlayerType>(), Moq.It.IsAny<BoardPosition>()))
				.Returns(true);
			Mocks.GetMock<IGameFactory>()
				.Setup(f => f.CreateFrom(Enums.PlayerType.Human, Moq.It.IsAny<BoardPosition>()))
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Human, BoardPosition.CreateFrom("A", 1)));
			gameSpec
				.Setup(s => s.IsGameOver(Moq.It.IsAny<Game>()))
				.Returns(true);
			Mocks.GetMock<IModelFactory>()
				.Setup(f => f.CreateFrom(Moq.It.IsAny<Game>()))
				.Returns(new GameModel 
				{
					IsGameOver = true
				});
			gameSpec
				.Setup(s => s.IsPlayerWinner(Moq.It.IsAny<Game>()))
				.Returns(false);
			gameSpec
				.Setup(s => s.IsComputerWinner(Moq.It.IsAny<Game>()))
				.Returns(false);
		};

		Because of = () => _result = ClassUnderTest.PerformMove(_request);

		It should_not_attempt_to_have_the_computer_perform_another_move = () =>
			Mocks.GetMock<IGameAlgorithms>()
				.Verify(a => a.DetermineNextMove(Moq.It.IsAny<Game>()), Times.Never());
	}
}