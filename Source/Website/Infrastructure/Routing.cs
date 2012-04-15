using System.Web.Mvc;
using System.Web.Routing;

namespace TTT.Website.Infrastructure
{
	public static class Routing
	{
		public static void Initialize(this RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}
	}
}