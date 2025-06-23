using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Areas.Kullanicilar.Controllers
{ 
    [Area("Kullanicilar")]
    public class MembersController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
