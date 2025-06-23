using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class Buyerr
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }
        public string IdentityNumber { get; set; } 
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string RegistrationAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }
        public List<ShippingAddres> ShippingAddress { get; set; }
        public List<Order> Orders { get; set; } 
        public BillingAddres BillingAddres { get; set; }
    }
}
