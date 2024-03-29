﻿using System.Collections.Generic;
using Machine.Specifications;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.GameLogic.Processes;
using TTT.Domain.GameLogic.Providers;
using TTT.Domain.GameLogic.Specifications;
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
			Mocks.GetMock<IComputerFirstTurnMoveProvider>()
				.Setup(c => c.GetCenterSquareMove())
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("B", 2)));
			Mocks.GetMock<IComputerFirstTurnSpecification>()
				.Setup(s => s.IsFirstTurnForComputer(Moq.It.IsAny<Game>()))
				.Returns(true);
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
			Mocks.GetMock<IComputerFirstTurnMoveProvider>()
				.Setup(c => c.GetCenterSquareMove())
				.Returns(new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("B", 2)));
			Mocks.GetMock<IComputerFirstTurnMoveProvider>()
				.Setup(c => c.GetCornerMoves())
				.Returns(new List<GameMove>
				{
					new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("A", 1)),
					new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("A", 3)),
					new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("C", 1)),
					new GameMoveBuilder().Build(Enums.PlayerType.Computer, BoardPosition.CreateFrom("C", 3)),
				});
			Mocks.GetMock<IComputerFirstTurnSpecification>()
				.Setup(s => s.IsFirstTurnForComputer(Moq.It.IsAny<Game>()))
				.Returns(true);
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
			Mocks.GetMock<IWinningMoveProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<IList<GameMove>>(), Enums.PlayerType.Computer))
				.Returns(new List<BoardPosition>());
			// available human moves
			Mocks.GetMock<IWinningMoveProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<IList<GameMove>>(), Enums.PlayerType.Human))
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
			Mocks.GetMock<IWinningMoveProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<IList<GameMove>>(), Enums.PlayerType.Computer))
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
			Mocks.GetMock<IWinningMoveProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<IList<GameMove>>(), Enums.PlayerType.Computer))
				.Returns(new List<BoardPosition>
				{
					BoardPosition.CreateFrom("B", 1)
				});
			// available winning human moves
			Mocks.GetMock<IWinningMoveProvider>()
				.Setup(p => p.GetPotentialWinningMovesFor(Moq.It.IsAny<IList<GameMove>>(), Enums.PlayerType.Human))
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
}