using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Controllers
{

    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BayiBasvuruFormu()
        {
            return View();
        }
        public IActionResult Detay()
        {
            return View();
        }
    }

}
