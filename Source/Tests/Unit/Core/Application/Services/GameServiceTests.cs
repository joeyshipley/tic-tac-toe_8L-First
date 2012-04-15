using Machine.Specifications;
using TTT.Core.Application.Services;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Core.Application.Services.GameServiceTests
{
	[Subject("Application, Services, GameService, Testing the testing classes")]
	public class When_attempting_to_setup_the_testing_classes
		: BaseIsolationTest<GameService>
	{
		private static bool _result;

		Establish context = () => 
		{
		};

		Because of = () =>
		{
			_result = ClassUnderTest.Test();
		};

		It should_be_all_good = () =>
			_result.ShouldBeTrue();
	}
}