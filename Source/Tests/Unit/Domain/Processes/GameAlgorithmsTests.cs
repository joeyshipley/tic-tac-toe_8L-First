using System.Collections.Generic;
using Machine.Specifications;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Processes;
using TTT.Core.Domain.Providers;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Processes.GameAlgorithmsTests
{
	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_players_first_move_is_not_on_the_middle_center_position
		: BaseIsolationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.DetermineNextMove(_game);

		It should_decide_to_return_a_move_for_the_middle_center_position = () =>
			_result.Position.Equals(BoardPosition.CreateFrom("B", 2));
	}

	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_players_first_move_is_on_the_middle_center_position
		: BaseIsolationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("B", 2) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.DetermineNextMove(_game);

		It should_decide_to_return_a_move_for_any_of_the_corner_positions = () =>
		{
			var isCornerPosition = _result.Position.Equals(BoardPosition.CreateFrom("A", 1))
				|| _result.Position.Equals(BoardPosition.CreateFrom("A", 3))
				|| _result.Position.Equals(BoardPosition.CreateFrom("C", 1))
				|| _result.Position.Equals(BoardPosition.CreateFrom("C", 3));
			isCornerPosition.ShouldBeTrue();
		};
	}

	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_choosing_a_move_for_the_computer_and_there_is_an_available_position_the_player_can_use_to_win
		: BaseIsolationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			// no available computer moves
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Computer))
				.Returns(new List<BoardPosition>());
			// available human moves
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Human))
				.Returns(new List<BoardPosition>
				{
					BoardPosition.CreateFrom("A", 3)
				});

			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("C", 3) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 2) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.DetermineNextMove(_game);

		It should_decide_to_take_the_board_position = () =>
			_result.Position.Equals(BoardPosition.CreateFrom("A", 3)).ShouldBeTrue();
	}
	
	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_choosing_a_move_for_the_computer_and_there_is_an_available_position_the_computer_can_use_to_win
		: BaseIsolationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Computer))
				.Returns(new List<BoardPosition>
				{
					BoardPosition.CreateFrom("B", 1)
				});

			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 3) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 3) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 3) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.DetermineNextMove(_game);

		It should_decide_to_take_the_board_position = () =>
			_result.Position.Equals(BoardPosition.CreateFrom("B", 1)).ShouldBeTrue();
	}
		
	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_choosing_a_move_for_the_computer_and_is_a_player_blocking_move_and_a_computer_winning_move_available
		: BaseIsolationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			// available winning computer moves
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Computer))
				.Returns(new List<BoardPosition>
				{
					BoardPosition.CreateFrom("B", 1)
				});
			// available winning human moves
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Human))
				.Returns(new List<BoardPosition>
				{
					BoardPosition.CreateFrom("A", 2)
				});

			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 3) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 3) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 3) }
			})
			.Build();
		};

		Because of = () => _result = ClassUnderTest.DetermineNextMove(_game);

		It should_decide_to_take_the_winning_move_instead_of_the_blocking_move = () =>
			_result.Position.Equals(BoardPosition.CreateFrom("B", 1)).ShouldBeTrue();
	}

	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_choosing_a_move_for_the_computer_and_there_is_no_winning_moves
		: BaseIsolationTest<GameAlgorithms>
	{
		private static GameMove _result;
		private static Game _game;

		Establish context = () => 
		{
			// no available winning computer moves
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Computer))
				.Returns(new List<BoardPosition>());
			// no available winning human moves
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Human))
				.Returns(new List<BoardPosition>());
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetRemainingAvailableBoardPositions(Moq.It.IsAny<Game>()))
				.Returns(new List<BoardPosition>
				{
					BoardPosition.CreateFrom("B", 1),
					BoardPosition.CreateFrom("B", 2),
					BoardPosition.CreateFrom("B", 3),
					BoardPosition.CreateFrom("C", 1),
					BoardPosition.CreateFrom("C", 2),
					BoardPosition.CreateFrom("C", 3)
				});
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

	[Subject("Domain, Processes, GameAlgorithms")]
	public class When_the_player_places_their_first_two_moves_in_opposite_corners
		: BaseIsolationTest<GameAlgorithms>
	{
		private static List<GameMove> _results = new List<GameMove>();
		private static Game _game;
		private static List<BoardPosition> _availablePositions = new List<BoardPosition>();

		Establish context = () => 
		{
			// no available winning computer moves
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Computer))
				.Returns(new List<BoardPosition>());
			// no available winning human moves
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<Game>(), Enums.PlayerType.Human))
				.Returns(new List<BoardPosition>());
			_availablePositions = new List<BoardPosition>
			{
				BoardPosition.CreateFrom("A", 2),
				BoardPosition.CreateFrom("A", 3),
				BoardPosition.CreateFrom("B", 1),
				BoardPosition.CreateFrom("B", 3),
				BoardPosition.CreateFrom("C", 1),
				BoardPosition.CreateFrom("C", 2)
			};
			Mocks.GetMock<IBoardPositionsProvider>()
				.Setup(p => p.GetRemainingAvailableBoardPositions(Moq.It.IsAny<Game>()))
				.Returns(_availablePositions);
			_game = new GameBuilder().WithMoves(new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 3) }
			})
			.Build();
		};

		Because of = () => 
		{
			for(var i = 0; i < _availablePositions.Count; i++)
			{
				Mocks.GetMock<IRandomNumberProvider>()
					.Setup(p => p.GenerateNumber(Moq.It.IsAny<int>(), Moq.It.IsAny<int>()))
					.Returns(i);
				_results.Add(ClassUnderTest.DetermineNextMove(_game));
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
}