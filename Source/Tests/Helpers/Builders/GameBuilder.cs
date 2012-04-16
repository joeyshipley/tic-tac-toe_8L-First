using System;
using System.Collections.Generic;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;

namespace TTT.Tests.Helpers.Builders
{
	public class GameBuilder
	{
		private Guid _id;
		private IList<GameMove> _moves;
		private bool _isGameOver;

		public GameBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public GameBuilder WithMoves(IList<GameMove> moves)
		{
			_moves = moves;
			return this;
		}

		public GameBuilder WithIsGameOver(bool isGameOver)
		{
			_isGameOver = isGameOver;
			return this;
		}

		public Game Build()
		{
			var game = new Game
			{
				Id = _id,
				Moves = _moves ?? new List<GameMove>(),
				IsGameOver = _isGameOver
			};
			return game;
		}

		public Game BuildGameWithValidMoves()
		{
			var moves = new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 1) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 2) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) }
			};

			var game = new Game
			{
				Id = _id,
				Moves =  moves,
				IsGameOver = _isGameOver
			};
			return game;
		}

		public Game BuildWithPossibleWinningMove()
		{
			var moves = new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 1) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 2) }
			};

			var game = new Game
			{
				Id = _id,
				Moves =  moves,
				IsGameOver = _isGameOver
			};
			return game;
		}

		public Game BuildWithMultiplePossibleWinningMove()
		{
			var moves = new List<GameMove>
			{
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("A", 1) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("B", 2) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 3) },
				new GameMove { Owner = Enums.PlayerType.Computer, Position = BoardPosition.CreateFrom("A", 3) },
				new GameMove { Owner = Enums.PlayerType.Human, Position = BoardPosition.CreateFrom("C", 1) }
			};

			var game = new Game
			{
				Id = _id,
				Moves =  moves,
				IsGameOver = _isGameOver
			};
			return game;
		}
	}
}