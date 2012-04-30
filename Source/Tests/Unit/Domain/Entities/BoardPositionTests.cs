using Machine.Specifications;
using TTT.Domain.Entities;

namespace TTT.Tests.Unit.Domain.Entities.BoardPositionTests
{
	[Subject("Domain, Entities, BoardPosition")]
	public class When_checking_to_see_if_two_board_positions_with_the_same_column_and_row_are_equal
	{
		private static bool _result;
		private static BoardPosition _original;
		private static BoardPosition _new;

		Establish context = () => 
		{
			_original = BoardPosition.CreateFrom("A", 1);
			_new = BoardPosition.CreateFrom("A", 1);
		};

		Because of = () => _result = _original.Equals(_new);

		It should_return_true = () => 
			_result.ShouldBeTrue();
	}

	[Subject("Domain, Entities, BoardPosition")]
	public class When_checking_to_see_if_two_board_positions_with_different_values
	{
		private static bool _result;
		private static BoardPosition _original;
		private static BoardPosition _new;

		Establish context = () => 
		{
			_original = BoardPosition.CreateFrom("A", 1);
			_new = BoardPosition.CreateFrom("B", 2);
		};

		Because of = () => _result = _original.Equals(_new);

		It should_return_false = () => 
			_result.ShouldBeFalse();
	}
}