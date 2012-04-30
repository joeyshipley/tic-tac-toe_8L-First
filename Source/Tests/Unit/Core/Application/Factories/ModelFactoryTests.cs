using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TTT.Application.Factories;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.Models;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Core.Application.Factories.ModelFactoryTests
{
	[Subject("Application, Factories, ModelFactory, GameModel creation")]
	public class When_asking_for_a_game_model
		: BaseIsolationTest<ModelFactory>
	{
		private static GameModel _result;
		private static Game _game;

		Establish context = () => 
		{
			var gameId = Guid.NewGuid();
			var moves = new List<GameMove>
			{
				new GameMove 
				{
					Id = Guid.NewGuid(),
					Owner = Enums.PlayerType.Human,
					Position = BoardPosition.CreateFrom("B", 2)
				}
			};
			_game = new GameBuilder()
				.WithId(gameId)
				.WithMoves(moves)
				.WithIsGameOver(true)
				.Build();
		};

		Because of = () => _result = ClassUnderTest.CreateFrom(_game);

		It should_return_a_game_model = () =>
			_result.ShouldNotBeNull();

		It should_set_the_models_id_to_match_the_game = () =>
			_result.GameId.ShouldEqual(_game.Id);

		It should_build_the_moves_from_the_games_moves = () =>
			_result.GameMoves.Count.ShouldEqual(_game.Moves.Count);

		It should_set_the_is_game_over_value = () =>
			_result.IsGameOver.ShouldBeTrue();
	}

	[Subject("Application, Factories, ModelFactory, GameModel creation")]
	public class When_asking_for_a_game_model_with_move_warnings
		: BaseIsolationTest<ModelFactory>
	{
		private static GameModel _result;
		private static Game _game;
		private static IList<ValidationError> _moveMessages;

		Establish context = () => 
		{
			var gameId = Guid.NewGuid();
			var moves = new List<GameMove>
			{
				new GameMove 
				{
					Id = Guid.NewGuid(),
					Owner = Enums.PlayerType.Human,
					Position = BoardPosition.CreateFrom("B", 2)
				}
			};
			_game = new GameBuilder()
				.WithId(gameId)
				.WithMoves(moves)
				.WithIsGameOver(false)
				.Build();

			_moveMessages = new List<ValidationError>
			{
				new ValidationError { Type = "InvalidMove" }
			};
		};

		Because of = () => _result = ClassUnderTest.CreateFrom(_game, _moveMessages);

		It should_set_the_games_move_warnings = () =>
			_result.MoveWarnings.Any().ShouldBeTrue();
	}
}