using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Areas.Kullanicilar.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
