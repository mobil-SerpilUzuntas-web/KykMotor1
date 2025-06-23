using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class BayiBasvuru
    {
        //[Key]
        public int Id { get; set; }
        public string AdiSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string FirmaAdi { get; set; }
        public string FirmaUnvani { get; set; }
        public string FirmaAdresi { get; set; }
        public string Sehir { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNumarasi { get; set; }
        public string Aciklama { get; set; }
        public DateTime BasvuruTarihi { get; set; }
        public string BasvuruDurumu { get; set; }

        // İlişki ekleyin
        public List<Notification> Notifications { get; set; }
    }
}
