﻿using System.Collections.Generic;
using StructureMap;
using TTT.Application.Repositories;
using TTT.Data.Repositories;

namespace TTT.Website.Infrastructure.IoC
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

		private static readonly IList<string> _assemblies = new List<string> 
		{ 
			"TTT.Application", 
			"TTT.Application.Models", 
			"TTT.Data", 
			"TTT.Domain",
			"TTT.Domain.GameLogic", 
			"TTT.Website" 
		}; 

		public static IContainer CreateContainer()
		{
			var container = iocConfiguration.CreateIoCContainer(_assemblies, (c => 
			{
				// NOTE: adding an expression, needs to be added in 
				// both CreateContainer() and InitializeObjectFactory().
				c.For<IGameRepository>().Use<CacheGameRepository>();
			}));
			return container;
		}

		public static void InitializeObjectFactory()
		{
			iocConfiguration.InitializeObjectFactory(_assemblies, (c => 
			{
				// NOTE: adding an expression, needs to be added in 
				// both CreateContainer() and InitializeObjectFactory().
				c.For<IGameRepository>().Use<CacheGameRepository>();
			}));
		}
	}
}