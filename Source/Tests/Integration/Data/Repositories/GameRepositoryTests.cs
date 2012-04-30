using System;
using Machine.Specifications;
using TTT.Domain.Entities;
using TTT.Data.Repositories;
using TTT.Tests.Helpers.Builders;
using TTT.Tests.Infrastructure;

namespace TTT.Tests.Integration.Data.Repositories
{
	[Subject("Data, Repositories, Game")]
	public class When_using_the_game_repository
		: BaseIntegrationTest<CacheGameRepository>
	{
		private static Game _fromStorageResult;
		private static Game _shouldntExistResult;
		private static Guid _id;
		private static Game _game;

		Establish context = () =>
		{
			_id = Guid.NewGuid();
			_game = new GameBuilder().WithId(_id).Build();
		};

		Because of = () => 
		{
			// add the game to storage
			ClassUnderTest.Save(_game);

			// fetch the game from storage
			_fromStorageResult = ClassUnderTest.Get(_id);

			// remove the game from storage
			ClassUnderTest.Remove(_game);

			// attempt to retrieve the removed game from storage
			_shouldntExistResult = ClassUnderTest.Get(_id);
		};

		It should_be_able_save_and_return_a_game_from_storage = () =>
			_fromStorageResult.Id.ShouldEqual(_id);

		It should_be_able_to_remove_a_game_from_storage = () =>
			_shouldntExistResult.ShouldBeNull();
	}
}