﻿using System.Collections.Generic;
using StructureMap;
using TTT.Core.Infrastructure.IoC;

namespace TTT.Website.Infrastructure
{
	public class IoCConfigurator
	{
		private static IoCConfiguration _iocConfiguration;
		private static IoCConfiguration iocConfiguration
		{
			get 
			{
				return _iocConfiguration ?? (_iocConfiguration = new IoCConfiguration());
			}
		}

		private static readonly IList<string> _assemblies = new List<string> { "TTT.Core", "TTT.Website" }; 

		public static IContainer CreateContainer()
		{
			var container = iocConfiguration.CreateIoCContainer(_assemblies, (c => 
			{
				// NOTE: adding an expression, needs to be added in 
				// both CreateContainer() and InitializeObjectFactory().
				//c.For<IErrorRepository>().Use<DbErrorRepository>();
			}));
			return container;
		}

		public static void InitializeObjectFactory()
		{
			iocConfiguration.InitializeObjectFactory(_assemblies, (c => 
			{
				// NOTE: adding an expression, needs to be added in 
				// both CreateContainer() and InitializeObjectFactory().
				//c.For<IErrorRepository>().Use<DbErrorRepository>();
			}));
		}
	}
}