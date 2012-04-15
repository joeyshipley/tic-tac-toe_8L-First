using AutoMoq;

namespace TTT.Tests.Infrastructure
{
	public class BaseIsolationTest<T>
		where T : class
	{
		protected readonly static AutoMoqer Mocks = new AutoMoqer();
		protected static T ClassUnderTest = Mocks.Resolve<T>();
	}
}