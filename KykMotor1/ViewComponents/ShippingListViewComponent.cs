using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.ViewComponents
{

    public class ShippingListViewComponent:ViewComponent
    {
        private readonly IShippingServices _shippingServices;
        private UserManager<ApplicationUser> _userManager;


        public ShippingListViewComponent(IShippingServices shippingServices, UserManager<ApplicationUser> userManager)
        {
            _shippingServices = shippingServices;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            string userId = userID ?? string.Empty; // Null kontrolü

            var shippingAddresses = await _shippingServices.GetAllShipping(userId) ?? new List<ShippingAddres>();
            return View(new BuyerModel()
            {
                shippingAddres = shippingAddresses

            });

        }
    }
}
