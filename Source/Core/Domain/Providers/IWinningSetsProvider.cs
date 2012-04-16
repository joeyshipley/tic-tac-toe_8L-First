using System.Collections.Generic;
using TTT.Core.Domain.Entities;

namespace TTT.Core.Domain.Providers
{
	public interface IWinningSetsProvider
	{
		IList<BoardPositionSet> GetWinningSets();
	}
}