using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Areas.Admin.Controllers
{

    [AllowAnonymous]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private INotificationServices _notificationServices;
        private IBuyerServices _buyerServices;
        private IBayiBasvurusuServices _bayiBasvurusuServices;
        public AdminController(INotificationServices notificationServices, IBuyerServices buyerServices, IBayiBasvurusuServices bayiBasvurusuServices)
        {
            _notificationServices = notificationServices;
            _buyerServices = buyerServices;
            _bayiBasvurusuServices = bayiBasvurusuServices;
        }
     
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        // GetUnreadNotificationsCount bu metot yeni bayi başvurusu yapılınca çalışıyor ve Index sayfasında ki  return Json(unreadCount);
        public async Task<IActionResult> GetUnreadNotificationsCount()
        {
            var unreadCount = await _notificationServices.GetUnreadNotificationsCountAsync();
            return Json(unreadCount);
        }

        [HttpPost]
        public IActionResult MarkNotificationsAsRead()
        {
            _notificationServices.MarkAllAsRead();
            return Ok();
        }

        [HttpGet]
        public IActionResult BayiBasvuruOku()
        {
            var notifications = _notificationServices.GetAll();
            var bayibasvuru = _bayiBasvurusuServices.GetAll();
            // Bildirimleri alın
            /*      var bayiBasvurular = _bayiBasvurusuServices.GetAll(); */// Başvuruları alın
            var viewModel = new BayiBasvuruOkuViewModel
            {
                Notifications = notifications,
                BayiBasvurular = bayibasvuru,
            };
            return View(viewModel);
        }


        // GET: Başvuru güncelleme formunu gösterme
        [HttpGet]
        public IActionResult BayiBasvuruGuncelle(int? id)
        {

            var bayiBasvuru = _bayiBasvurusuServices.GetById((int)id); // Veritabanından ilgili başvuru bilgilerini çekiyoruz
            if (bayiBasvuru == null)
            {
                return NotFound(); // Eğer başvuru bulunamazsa 404 döndür
            }
            var model = new BayiBasvuruModel()
            {
                BasvuruDurumu = bayiBasvuru.BasvuruDurumu,
            };
            return View(model); // Başvuru bilgilerini View'e gönder
        }

        // POST: Güncellenmiş başvuru durumunu kaydetme
        [HttpPost]
        public IActionResult BayiBasvuruGuncelle(BayiBasvuruModel model)
        {

            var basvuru = _bayiBasvurusuServices.GetById(model.Id);
            if (basvuru != null)
            {
                basvuru.BasvuruDurumu = model.BasvuruDurumu;
                _bayiBasvurusuServices.Update(basvuru);
                return RedirectToAction("Index");
            }
            return View(model);

            //return View(model);
            // Güncellemeden sonra liste sayfasına yönlendir
        }



        [HttpGet]
        public IActionResult BayiBasvuruDetay(int id)
        {

            var bayiBasvuru = _bayiBasvurusuServices.GetById(id);
            if (bayiBasvuru == null)
            {
                return NotFound();
            }

            return View(bayiBasvuru);
        }

    }
}
