using KykMotorApp.WebIU.Areas.Admin.Models;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KykMotorApp.WebIU.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> rolManager, SignInManager<ApplicationUser> signInManager)
        {

            _userManager = userManager;
            _roleManager = rolManager;
            _signInManager = signInManager;
        }
   
        public async Task<IActionResult> UserList()
        {
            // Tüm kullanıcıları çek
            var allUsers = await _userManager.Users.ToListAsync();

            // View'e tüm kullanıcıları gönder
            return View(allUsers);
        }
        //public async Task<IActionResult> UserList()
        //{
        //    // Sadece aktif kullanıcıları çek
        //    var activeUsers = await _userManager.Users
        //      /*  .Where(user => user.IsActive)*/ // Aktif kullanıcıları filtrele
        //        .ToListAsync();

        //    // Aktif kullanıcıları UserListViewModel'e dönüştür
        //    var userListViewModel = activeUsers.Select(user => new UserListViewModel
        //    {
        //        UserName = user.UserName,
        //        Name = user.Name,
        //        SurName = user.SurName,
        //        Aktif = user.IsActive
        //    }).ToList();

        //    // View'e ViewModel'i gönder
        //    return View(userListViewModel);
        //}

        // User Detayları için Action
   
        public async Task<IActionResult> UserDetay(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // User bilgilerini çekiyoruz
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // View modelini dolduruyoruz
            var userDetayView = new UserDetayView
            {
                UserName= user.UserName,
                Name = user.Name,
                SurName = user.SurName,
                City = user.Ctiy,
                Phone=user.PhoneNumber,
                Email= user.Email,
                FirmaAdı = user.FirmaAdı,
                VergiDairesi = user.VergiDairesi,
                VergiNo = user.VergiNo,
                IsActive = user.IsActive
            };

            return View(userDetayView);
        }

        //public async Task<IActionResult> UserDelete(string id)
        //{
        //    // Kullanıcıyı bul
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();  // Kullanıcı bulunamadı
        //    }


        //    // Kullanıcıyı pasif hale getir
        //    user.IsActive = false;
        //    var result = await _userManager.UpdateAsync(user);

        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("UserList"); // Listeye geri dön
        //    }

        //    // Hata varsa bir mesaj gösterilebilir
        //    ModelState.AddModelError("", "Kullanıcı silinirken bir hata oluştu.");
        //    return View("UserList");
        //}
     
        [HttpPost]
        public async Task<IActionResult> UserDelete(string id)
        {
            // Kullanıcıyı bul
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            // Kullanıcıyı pasif hale getir
            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // İşlem başarılı olursa success true ve bir mesaj döndür
                return Json(new { success = true });
            }

            // Hata durumunda success false ve hata mesajı döndür
            return Json(new { success = false, message = "Kullanıcı silinirken bir hata oluştu." });
        }
      
        public IActionResult UserUpdate()
        {
            return View(new UserUpdateViewModel());
        }
     
        [HttpPost]
        public IActionResult UserUpdate(UserUpdateViewModel model)
        {
        
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
      

    }


}

