using System;
using System.Collections.Generic;
using TTT.Domain.Entities;
using TTT.Domain.Helpers;

namespace TTT.Domain.GameLogic.Providers
{
	public class ComputerFirstTurnMoveProvider : IComputerFirstTurnMoveProvider
	{
		public GameMove GetCenterSquareMove()
		{
			var halfOfMaxNumberOfSquares = (decimal) Constants.BoardPositionRowLength / 2;
			var roundedUpHalfWayPoint = (int) Math.Round(halfOfMaxNumberOfSquares);
			return GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom(roundedUpHalfWayPoint.ToAlphabet(), roundedUpHalfWayPoint));
		}

		public IList<GameMove> GetCornerMoves()
		{
			var moves = new List<GameMove>
			{
				GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom(1.ToAlphabet(), 1)),
				GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom(1.ToAlphabet(), Constants.BoardPositionRowLength)),
				GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom(Constants.BoardPositionRowLength.ToAlphabet(), 1)),
				GameMove.CreateFrom(Enums.PlayerType.Computer, BoardPosition.CreateFrom(Constants.BoardPositionRowLength.ToAlphabet(), Constants.BoardPositionRowLength))
			};
			return moves;
		}
	}
}