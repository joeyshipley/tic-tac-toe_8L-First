using System.Web.Mvc;
using TTT.Core.Application.Request;
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
			return View();
		}

		public ActionResult Play()
		{
			var model = _gameService.New();
			return View(model);
		}

		public JsonResult PerformMove(PerformMoveRequest request)
		{
			var model = _gameService.PerformMove(request);
			return Json(model, JsonRequestBehavior.AllowGet);
		}
	}
}
