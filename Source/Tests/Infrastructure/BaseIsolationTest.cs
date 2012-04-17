using AutoMoq;
using Machine.Specifications;

namespace TTT.Tests.Infrastructure
{
	public class BaseIsolationTest<T>
		where T : class
	{
		protected static AutoMoqer Mocks;
		protected static T ClassUnderTest;

		Establish context = () =>
		{
			Mocks = new AutoMoqer();
			ClassUnderTest = Mocks.Resolve<T>();
		};
	}
}