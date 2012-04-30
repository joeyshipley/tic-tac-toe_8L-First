﻿using System;
using System.Linq;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.Factories;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Factories.GameFactoryTests
{
	[Subject("Domain, Factories, GameFactory, Game creation")]
	public class When_asking_for_a_game
		: BaseIsolationTest<GameFactory>
	{
		private static Game _result;

		Establish context;

		Because of = () => _result = ClassUnderTest.CreateNew();

		It should_create_a_game = () =>
			_result.ShouldNotBeNull();

		It should_assign_the_game_an_id = () =>
			_result.Id.ShouldNotEqual(Guid.Empty);

		It should_assing_an_empty_list_of_moves = () =>
		{
			_result.Moves.ShouldNotBeNull(); 
			_result.Moves.Any().ShouldBeFalse();
		};
	}

	[Subject("Domain, Factories, GameFactory, Game creation")]
	public class When_asking_for_a_game_move
		: BaseIsolationTest<GameFactory>
	{
		private static GameMove _result;
		private static Enums.PlayerType _owner;
		private static BoardPosition _position;

		Establish context = () => 
		{
			_owner = Enums.PlayerType.Human;
			_position = BoardPosition.CreateFrom("B", 2);
		};

		Because of = () => _result = ClassUnderTest.CreateFrom(_owner, _position);

		It should_create_a_game_move = () =>
			_result.ShouldNotBeNull();

		It should_assign_the_game_an_id = () =>
			_result.Id.ShouldNotEqual(Guid.Empty);

		It should_assign_the_owner = () =>
			_result.Owner.ShouldEqual(_owner);

		It should_assign_the_position = () =>
			_result.Position.ShouldEqual(_position);
	}
}