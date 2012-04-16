using System;

namespace TTT.Core.Domain.Entities
{
	public class BoardPosition
	{
		public string Column { get; set; }
		public int Row { get; set; }

		public static BoardPosition CreateFrom(string column, int row)
		{
			var boardPosition = new BoardPosition
			{
				Column = column,
				Row = row
			};
			return boardPosition;
		}

		public override bool Equals(object obj)
		{
 			var isSameClass = obj is BoardPosition;
			if(!isSameClass)
				return false;

			var compareTo = (BoardPosition) obj;
			return Column.Equals(compareTo.Column, StringComparison.OrdinalIgnoreCase)
				&& Row == compareTo.Row;
		}
	}
}