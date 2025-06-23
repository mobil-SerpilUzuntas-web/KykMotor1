using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult UserList()
		{
			return View();
		}
	}
}
