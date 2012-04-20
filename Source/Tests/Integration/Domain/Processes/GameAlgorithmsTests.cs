using System.Collections.Generic;
using System.Threading;
using Machine.Specifications;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Processes;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Integration.Domain.Processes.GameAlgorithmsTests
{
	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_player_places_their_first_two_moves_in_opposite_corners
		: BaseIntegrationTest<GameAlgorithms>
	{
		private static List<GameMove> _results = new List<GameMove>();
		private static Game _game;
		private static List<BoardPosition> _availablePositions = new List<BoardPosition>();

		Establish context = () => 
		{
			_availablePositions = new List<BoardPosition>
			{
				BoardPosition.CreateFrom("A", 2),
				BoardPosition.CreateFrom("A", 3),
				BoardPosition.CreateFrom("B", 1),
				BoardPosition.CreateFrom("B", 3),
				BoardPosition.CreateFrom("C", 1),
				BoardPosition.CreateFrom("C", 2)
			};
		};

		Because of = () => 
		{
			for(var i = 0; i < 25; i++)
			{
				_game = new GameBuilder().WithMoves(new List<GameMove>
				{
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
					new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) },
					new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 3) }
				})
				.Build();
				_results.Add(ClassUnderTest.DetermineNextMove(_game));
				Thread.Sleep(10);
			}
		};

		It should_not_let_the_computer_pick_a_corner_move = () =>
		{
			foreach(var result in _results)
			{
				var isTopRightCorner = result.Position.Equals(BoardPosition.CreateFrom("C", 1));
				var isBottomLeftCorner = result.Position.Equals(BoardPosition.CreateFrom("A", 3));
				(isTopRightCorner || isBottomLeftCorner).ShouldBeFalse();
			}
		};
	}

	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_choosing_a_move_for_the_computer_and_there_is_no_winning_moves
		: BaseIntegrationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("A", 2) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 3) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.DetermineNextMove(_game);

		It should_return_a_move = () =>
			_result.ShouldNotBeNull();

		It should_not_return_a_choosen_position = () =>
			_result.Position.Equals(BoardPosition.CreateFrom("A", 1)).ShouldBeFalse();
	}
}