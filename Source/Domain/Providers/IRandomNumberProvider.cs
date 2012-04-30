namespace TTT.Domain.Providers
{
	public interface IRandomNumberProvider
	{
		int GenerateNumber(int min, int max);
	}
}