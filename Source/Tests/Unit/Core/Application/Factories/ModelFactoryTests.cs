using System;
using System.Collections.Generic;
using Machine.Specifications;
using TTT.Core.Application.Factories;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Models;
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
					Position = Enums.BoardPosition.MiddleCenter
				}
			};
			_game = new GameBuilder()
				.WithId(gameId)
				.WithMoves(moves)
				.Build();
		};

		Because of = () => _result = ClassUnderTest.CreateFrom(_game);

		It should_return_a_game_model = () =>
			_result.ShouldNotBeNull();

		It should_set_the_models_id_to_match_the_game = () =>
			_result.GameId.ShouldEqual(_game.Id);

		It should_build_the_moves_from_the_games_moves = () =>
			_result.GameMoves.Count.ShouldEqual(_game.Moves.Count);
	}
}