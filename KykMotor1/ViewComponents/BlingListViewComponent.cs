using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.ViewComponents
{
    
    public class BlingListViewComponent:ViewComponent
    {
        private readonly IBillingServices _billingServices;
        private UserManager<ApplicationUser> _userManager;



        public BlingListViewComponent(IBillingServices billingServices, UserManager<ApplicationUser> userManager)
        {
            _billingServices = billingServices;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userID =  _userManager.GetUserId(HttpContext.User);
            string userId = userID ?? string.Empty; // Null kontrolü

            var billingAdress = await _billingServices.GetAllBilling(userId)?? new BillingAddres();


            return View(new BuyerModel()
            {
                billingAddres = billingAdress

            }); 

        }
    }
}
