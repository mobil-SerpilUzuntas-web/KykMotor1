using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Models
{
    public class HataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
