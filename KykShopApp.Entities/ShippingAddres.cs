using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    //Müşteri adresi
    public class ShippingAddres
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        // User ilişkisi için
        public string UserId { get; set; }
        public int BuyerId { get; set; }
        public Buyerr Buyerr { get; set; }

        public static implicit operator List<object>(ShippingAddres v)
        {
            throw new NotImplementedException();
        }
        //ShippingAddress Bagımlıdır neye Order bu yüzden dependent entity(Bagımlı Entity)Kısmına OrderId veriyoruz.
        //public int OrderId { get; set; }
        //public Order Order { get; set; }


    }
}
