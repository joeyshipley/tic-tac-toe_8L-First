using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TTT.Domain.Entities;
using TTT.Domain.Providers;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Unit.Domain.Providers.WinningPositionProviderTests
{
	[Subject("Domain, Providers, WinningSetsProvider")]
	public class When_asking_for_the_possible_winning_combinations
		: BaseIsolationTest<WinningSetsProvider>
	{
		private static IList<BoardPositionSet> _result;

		Establish context;

		Because of = () => _result = ClassUnderTest.GetWinningSets();

		It should_return_a_set_for_the_top_row = () =>
			_result.Any(s => 
				s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("A", 1)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("A", 2)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("A", 3)))
			)
			.ShouldBeTrue();

		It should_return_a_set_for_the_middle_row = () =>
			_result.Any(s => 
				s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("B", 1)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("B", 2)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("B", 3)))
			)
			.ShouldBeTrue();

		It should_return_a_set_for_the_bottom_row = () =>
			_result.Any(s => 
				s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("C", 1)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("C", 2)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("C", 3)))
			)
			.ShouldBeTrue();

		It should_return_a_set_for_the_left_column = () =>
			_result.Any(s => 
				s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("A", 1)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("B", 1)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("C", 1)))
			)
			.ShouldBeTrue();

		It should_return_a_set_for_the_center_column = () =>
			_result.Any(s => 
				s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("A", 2)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("B", 2)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("C", 2)))
			)
			.ShouldBeTrue();

		It should_return_a_set_for_the_right_column = () =>
			_result.Any(s => 
				s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("A", 3)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("B", 3)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("C", 3)))
			)
			.ShouldBeTrue();

		It should_return_a_set_for_the_top_left_to_bottom_right_diagonal = () =>
			_result.Any(s => 
				s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("A", 1)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("B", 2)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("C", 3)))
			)
			.ShouldBeTrue();

		It should_return_a_set_for_the_bottom_left_to_top_right_diagonal = () =>
			_result.Any(s => 
				s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("C", 1)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("B", 2)))
				&& s.Positions.Any(p => p.Equals(BoardPosition.CreateFrom("A", 3)))
			)
			.ShouldBeTrue();
	}
}