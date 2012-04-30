namespace TTT.Domain.GameLogic.Providers
{
	public interface IRandomNumberProvider
	{
		int GenerateNumber(int min, int max);
	}
}