﻿using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Models
{
	public class BoardPositionModel
	{
		public string Column { get; set; }
		public int Row { get; set; }

		public static BoardPositionModel CreateFrom(BoardPosition position)
		{
			var model = new BoardPositionModel
			{
				Column = position.Column,
				Row = position.Row
			};
			return model;
		}
	}
}