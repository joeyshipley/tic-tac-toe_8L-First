using System.Web.Mvc;
using TTT.Core.Application.Services;

namespace TTT.Website.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGameService _gameService;

		public HomeController(IGameService gameService)
		{
			_gameService = gameService;
		}

		public ActionResult Index()
		{
			var response = _gameService.Test();
			return View();
		}
	}
}
