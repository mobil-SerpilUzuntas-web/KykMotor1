
using KykMotorApp.WebIU.CustomValidator;
using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
	{
		private ICartServices _cartServices;
        private UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		private SignInManager<ApplicationUser> _signInManager;
        private IBayiBasvurusuServices _bayiBasvurusuServices;
        private INotificationServices _notificationServices;

        public AccountController(ICartServices cartServices, IBayiBasvurusuServices bayiBasvuruServisi,INotificationServices notificationServices ,UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> rolManager, SignInManager<ApplicationUser> signInManager)
		{
            _notificationServices= notificationServices;
            _bayiBasvurusuServices= bayiBasvuruServisi;  
            _cartServices= cartServices;
            _userManager = userManager;
			_roleManager = rolManager;
			_signInManager = signInManager;
		}
       
        [HttpGet]
        public IActionResult BayiBasvuru()
		{
			return View();
		}
      
        [HttpPost]
        public async Task<IActionResult> BayiBasvuru(BayiBasvuruModel model)
        {

            // Model verilerini BayiBasvuru entity'sine dönüştür
            var bayiBasvuru = new BayiBasvuru
            {
                AdiSoyadi = model.AdiSoyadi,
                Telefon = model.Telefon,
                Email = model.Email,
                FirmaAdi = model.FirmaAdi,
                FirmaUnvani = model.FirmaUnvani,
                FirmaAdresi = model.FirmaAdresi,
                Sehir = model.Şehir,
                VergiDairesi = model.VergiDairesi,
                VergiNumarasi = model.VergiNumarasi,
                Aciklama = model.Aciklama,
                BasvuruTarihi = DateTime.Now,
                BasvuruDurumu = "Beklemede" // Varsayılan değer
            };


            await _bayiBasvurusuServices.CreateAsync(bayiBasvuru);


            // Bildirim oluşturma
            var notification = new Notification
            {
                BayiBasvuruId = bayiBasvuru.Id,
                Message = "Yeni bir bayi başvurusu yapıldı."
            };
           await _notificationServices.CreateAsync(notification); // Bildirimi kaydet
                                                        // Başarılı mesajı ve yönlendirme
            TempData["SuccessMessage"] = "Başvurunuz başarıyla alınmıştırtır ";
            return RedirectToAction("BayiBasvuru");
        }

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
     
        public IActionResult SignUp()
		{
			return View(new AccountRegisterModel());
		}
       
        [HttpPost]
		public async Task<IActionResult> SignUp(AccountRegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = new ApplicationUser()
			{
				Name = model.Name,
				SurName = model.SurName,
				PhoneNumber = model.Phone,
				Ctiy = model.Ctiy,
				Email = model.Email,
				FirmaAdı = model.FirmaAdı,
				VergiDairesi = model.VergiDairesi,
				VergiNo = model.VergiNo,
				UserName = model.UserName,
				IsActive = true,
			};

			// Kullanıcı adı için doğrulama validation 
			  foreach (var validator in _userManager.UserValidators)
			   {
				var userValidationResult = await validator.ValidateAsync(_userManager, user);
				if (!userValidationResult.Succeeded)
				{
					foreach (var error in userValidationResult.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View(model);
				}
			    }
		
			var user1 = await _userManager.FindByNameAsync(model.UserName);
		
			if (user1 != null) // Hem email hem de kullanıcı adı var mı kontrolü
			{
				// Hem kullanıcı adı hem de email mevcut
				if (user1.UserName == model.UserName && user1.Email == model.Email )
				{
					ViewBag.ErrorMessage = "Bu kullanıcı adı ve e-posta adresi zaten kayıtlı!";
					return View(model);
				}
				else if(user1.UserName == model.UserName)
				{
					ViewBag.ErrorMessage = "Bu kullanıcı adı zaten kayıtlı!"; 
					return View(model);
				}

				else if (user1.Email == model.Email)
				{
					ViewBag.ErrorMessage = "Bu e-posta adresi zaten kayıtlı!";
					return View(model);
				}
			}
		

			if (model.Password == model.ConfirmPassword
				)
			{
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
                    _cartServices.IntializeCart(user.Id);

                    return RedirectToAction("SingIn", "Account");
				}
				//Eğer başarısız olduysa, hataları ModelState'e ekle

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				
			}

			return View(model);
		}
        //Login kısmı Get metodu
      
        [HttpGet]
        public IActionResult SingIn()
		{
			return View(new AccountLoginModel());
		}
      
        //Login kısmı post metodu
        [HttpPost]
		public async Task<IActionResult> SingIn(AccountLoginModel model)
		{
			if(!ModelState.IsValid) 
			{ 
				return View(model); 
			}
			var user = await _userManager.FindByNameAsync(model.UserName);

		   if (user == null)
		   {
			ModelState.AddModelError("", "Böyle bir kullanıcı yoktur Lütfen Yeni Bir Kullanıcı Oluşturun");
			return View(model);
	    	}
			if (user.IsActive == true)
			{
				var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					
					  
                    var loggedUser = await _userManager.FindByNameAsync(model.UserName); // Giriş yapan kullanıcıyı bul
                    var roles = await _userManager.GetRolesAsync(loggedUser); // Kullanıcının rollerini al

                    if (roles.Contains("admin"))
                    {
                        //return RedirectToAction("Dashboard", "Admin", new { area = "Admin" });
                        return RedirectToAction("Index", "Home");
                    }
                    else if (roles.Contains("standart"))
                    {
                        return RedirectToAction("Index", "Home");
                        //return RedirectToAction("Dashboard", "Members", new { area = "Kullanicilar" });
                    }

                }

            }
			else
			{
				ModelState.AddModelError("", "Bu Kullanıcı Silinmiştir Lütfen Bilgi Almak İçin Bize Ulaşınız: 05659684545 ");
				return View(model);
			}
			return View();
		}

	
        public async Task<IActionResult> Logoult()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
        public async Task<IActionResult> UserDelete1(string id)
        {
            // Kullanıcıyı bul
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();  // Kullanıcı bulunamadı
            }


            // Kullanıcıyı pasif hale getir
            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("UserList"); // Listeye geri dön
            }

            // Hata varsa bir mesaj gösterilebilir
            ModelState.AddModelError("", "Kullanıcı silinirken bir hata oluştu.");
            return View("UserList");
        }


        [HttpPost]
        public async Task<IActionResult> ResetPasswordAdmin(string userId, string newPassword)
        {
            // UserId'ye göre kullanıcıyı bul
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            // Yeni şifreyi belirleyelim
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Role", new { area = "Admin" }); // Kullanıcıları listeleyen sayfaya yönlendir
            }

            // Hata durumunda aynı sayfaya hatayı geri döndür
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();

        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }

}







