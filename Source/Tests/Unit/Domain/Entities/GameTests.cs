using System;
using System.Linq;
using Machine.Specifications;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Entities.GameTests
{
	[Subject("Domain, Entities, Game")]
	public class When_add_a_move_to_a_game
		: BaseIsolationTest<Game>
	{
		private static Game _game;
		private static GameMove _move;
		private static Guid _gameMoveId;

		Establish context = () => 
		{
			_gameMoveId = Guid.NewGuid();
			_game = new GameBuilder().Build();
			_move = new GameMoveBuilder().WithId(_gameMoveId).Build(Enums.PlayerType.Human, Enums.BoardPosition.MiddleCenter);
		};

		Because of = () => _game.AddMove(_move);

		It should_add_it_to_the_move_collection = () => 
			_game.Moves.Any(m => m.Id == _gameMoveId).ShouldBeTrue();
	}
}