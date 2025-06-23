using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    //fatura adres

    public class BillingAddres
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }
        public string TcKimlik { get; set; }
        public string VergiDairesi { get; set; }
        public string FirmaAdi { get; set; }

        //BilingAdress Bagımlıdır neye Order bu yüzden dependent entity(Bagımlı Entity)Kısmına OrderId veriyoruz.
        //public int OrderId { get; set; }
        //public Order Order { get; set; }

        //BilingAdress Bagımlıdır neye Buyerr bu yüzden dependent entity(Bagımlı Entity)Kısmına BuyerId veriyoruz.
         public int BuyerId { get; set; }
         public Buyerr Buyerr { get; set; }

        public static implicit operator List<object>(BillingAddres v)
        {
            throw new NotImplementedException();
        }
    }
}
