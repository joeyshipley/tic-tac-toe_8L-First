using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TTT.Website.Infrastructure;
using TTT.Website.Infrastructure.IoC;

namespace TTT.Website
{
	public class MvcApplication : HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.Initialize();
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			InitializeIoCContainer();
		}

		public void InitializeIoCContainer()
		{
			var container = IoCConfigurator.CreateContainer();
			DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
		}
	}
}