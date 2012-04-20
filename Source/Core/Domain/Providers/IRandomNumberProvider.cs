namespace TTT.Core.Domain.Providers
{
	public interface IRandomNumberProvider
	{
		int GenerateNumber(int min, int max);
	}
}