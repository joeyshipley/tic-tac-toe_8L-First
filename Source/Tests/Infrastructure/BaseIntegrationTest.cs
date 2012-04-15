using Machine.Specifications;
using StructureMap;
using TTT.Website.Infrastructure;

namespace TTT.Tests.Infrastructure
{
	public class BaseIntegrationTest<T>
		where T : class
	{
		private static bool _isTestObjectFactoryInitialized;

		protected static T ClassUnderTest;

		Establish context = () => 
		{
			if(!_isTestObjectFactoryInitialized) 
			{
				IoCConfigurator.InitializeObjectFactory();
				_isTestObjectFactoryInitialized = true;
			}

			ClassUnderTest = ObjectFactory.GetInstance<T>();
		};
	}
}