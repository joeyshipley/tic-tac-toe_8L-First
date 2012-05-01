using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace TTT.Website.Infrastructure.IoC
{
	public class IoCConfiguration
	{
		public delegate void ContainerCreationSpecificRegistrations(ConfigurationExpression expression);
		public delegate void ObjectFactoryInitializationSpecificRegistrations(IInitializationExpression expression);

		public IContainer CreateIoCContainer(IList<string> assembliesToRegisterFrom, ContainerCreationSpecificRegistrations containerCreationSpecificRegistrations)
		{
			var assemblies = buildAssembliesList(assembliesToRegisterFrom);

			IContainer container = new Container(c => 
			{
				c.Scan(x => 
				{
					assemblies.ForEach(x.Assembly);
					x.TheCallingAssembly();
					x.WithDefaultConventions();
					x.LookForRegistries();
				});

				containerCreationSpecificRegistrations(c);
			});
			return container;
		}

		public void InitializeObjectFactory(IList<string> assembliesToRegisterFrom, ObjectFactoryInitializationSpecificRegistrations objectFactoryInitializationSpecificRegistrations)
		{
			var assemblies = buildAssembliesList(assembliesToRegisterFrom);

			ObjectFactory.Initialize(c =>
			{
				c.Scan(x => 
				{
					assemblies.ForEach(x.Assembly);
					x.TheCallingAssembly();
					x.WithDefaultConventions();
					x.LookForRegistries();
				});

				objectFactoryInitializationSpecificRegistrations(c);
			});
		}

		private List<string> buildAssembliesList(IList<string> assembliesToRegisterFrom)
		{
			var localAssemblies = new List<string> {};

			var assemblies = new List<string>();
			assemblies.AddRange(localAssemblies);
			assemblies.AddRange(assembliesToRegisterFrom);
			assemblies = assemblies.Distinct().ToList();

			return assemblies;
		}
	}
}