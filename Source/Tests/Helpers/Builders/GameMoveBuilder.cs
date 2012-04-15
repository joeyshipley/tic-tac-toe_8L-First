using System;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;

namespace TTT.Tests.Helpers.Builders
{
	public class GameMoveBuilder
	{
		private Guid _id;

		public GameMoveBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public GameMove Build(Enums.PlayerType owner, Enums.BoardPosition position)
		{
			var move = new GameMove
			{
				Id = _id,
				Owner = owner,
				Position = position
			};
			return move;
		}
	}
}