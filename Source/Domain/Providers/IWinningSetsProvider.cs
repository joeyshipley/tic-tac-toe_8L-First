using System.Collections.Generic;
using TTT.Domain.Entities;

namespace TTT.Domain.Providers
{
	public interface IWinningSetsProvider
	{
		IList<BoardPositionSet> GetWinningSets();
	}
}