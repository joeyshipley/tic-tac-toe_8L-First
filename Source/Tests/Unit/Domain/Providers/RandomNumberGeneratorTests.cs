using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Machine.Specifications;
using TTT.Domain.GameLogic.Providers;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Providers
{
	[Subject("Domain, Providers, RandomNumberGenerator")]
	public class When_asking_for_numbers_to_be_randomly_generated
		: BaseIntegrationTest<RandomNumberProvider>
	{
		private static List<int> _result;
		private static int _min;
		private static int _max;

		Establish context = () => 
		{
			_min = 1;
			_max = 5;
			_result = new List<int>();
		};

		Because of = () => 
		{
			for(var i = 0; i < 25; i++)
			{
				_result.Add(ClassUnderTest.GenerateNumber(_min, _max));
				Thread.Sleep(10);
			}
		};

		It should_not_return_numbers_under_the_min_value_passed_in = () =>
			_result.Any(rn => rn < _min).ShouldBeFalse();

		It should_not_return_numbers_over_the_max_value_passed_in = () =>
			_result.Any(rn => rn > _max).ShouldBeFalse();
	}
}