using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.ViewComponents
{
    public class NavbarListViewComponent:ViewComponent
    {
        private readonly ICartServices _cartServices;
        private UserManager<ApplicationUser> _userManager;


        public NavbarListViewComponent(ICartServices cartServices, UserManager<ApplicationUser> userManager)
        {
            _cartServices = cartServices;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User); // Önce user çekiyoruz
            //string userId = await _userManager.GetUserIdAsync(user); // Sonra userId çekiyoruz
            // Kullanıcının kimlik bilgilerini al
            var userID =  _userManager.GetUserId(HttpContext.User);
            
              
            string userId = userID; // Kullanıcı giriş yapmışsa Id al, yapmamışsa null olur

            int quantity = 0;

            if (!string.IsNullOrEmpty(userId))
            {
                quantity = _cartServices.GetQuantityByUserId(userId);
            }

            return View(new CartItemModel()
            {
                Quantity = quantity
            });
        }
    }
}
