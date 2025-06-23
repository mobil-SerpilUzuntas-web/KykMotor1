using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{
    public class BuyerModel
    {

            public int BuyerId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string GsmNumber { get; set; }
            public string IdentityNumber { get; set; }
            public DateTime RegistrationDate { get; set; }= DateTime.Now;
            public DateTime LastLoginDate { get; set; }=DateTime.Now;
            public string RegistrationAddress { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string ZipCode { get; set; }
            public string UserId { get; set; }
           public List<ShippingAddres> shippingAddres { get; set; }
           public BillingAddres billingAddres { get; set; }

    }
    
}
