namespace TTT.Domain.Helpers
{
	public static class ExtensionMethods
	{
		public static string ToAlphabet(this int numeric)
		{
			return ((char)(65 + (numeric - 1))).ToString();
		}
	}
}