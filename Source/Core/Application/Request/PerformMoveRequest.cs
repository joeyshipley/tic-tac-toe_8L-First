using System;

namespace TTT.Core.Application.Request
{
	public class PerformMoveRequest
	{
		public Guid GameId { get; set; }
		public string SelectedColumn { get; set; }
		public int SelectedRow { get; set; }
	}
}