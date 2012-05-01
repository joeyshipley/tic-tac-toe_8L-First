﻿using System;
using System.Collections.Generic;

namespace TTT.Application.Models
{
	public class GameModel
	{
		public Guid GameId { get; set; }
		public IList<GameMoveModel> GameMoves { get; set; }
		public bool IsGameOver { get; set; }
		public bool IsPlayerWinner { get; set; }
		public bool IsComputerWinner { get; set; }
		public bool IsGameDraw { get; set; }

		public IList<ValidationError> MoveWarnings { get; set; } 
	}
}