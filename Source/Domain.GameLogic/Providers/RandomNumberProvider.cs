using System;

namespace TTT.Domain.GameLogic.Providers
{
	public class RandomNumberProvider : IRandomNumberProvider
	{
		public int GenerateNumber(int min, int max)
		{
			var random = new Random();
			var randomNumber = random.Next(min, max);
			return randomNumber;
		}
	}
}