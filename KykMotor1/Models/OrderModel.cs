using KykShopApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace KykMotorApp.WebIU.Models
{
    public class OrderModel
    {
        public int buyerId { get; set; }
        [Display(Name = "Kart Kime Ayit")]
        public string CartName { get; set; }
        [Display(Name = "Kart numarası")]
        public string CartNumber { get; set; }
        [Display(Name = "Kart Ay")]
        public string ExpirationMonth { get; set; }
        [Display(Name = "Kart Yıl")]
        public string ExpirationYear  { get; set; }
        [Display(Name = "Kart Cvv")]
        public string Cvv { get; set; }
        public string OrderNote { get; set; }
        public CartModel CartModel { get; set; }
        public List<BuyerModel> BuyerModel { get; set; }
        public List<ShippingModel> ShippingModel { get; set; }
        public List<BillingModel> BillingModel { get; set; }
        //public Buyerr Buyerr { get; set; }
    }
}

