using KykShopApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace KykMotorApp.WebIU.Models
{
    public class BayiBasvuruOkuViewModel
    {
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string AdiSoyadi { get; set; }

        //[Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
        //public string LastName { get; set; }

        [Required(ErrorMessage = "Telefon alanı boş bırakılamaz.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Firma adı alanı boş bırakılamaz.")]
        public string FirmaAdi { get; set; }

        [Required(ErrorMessage = "Firma ünvanı alanı boş bırakılamaz.")]
        public string FirmaUnvani { get; set; }

        [Required(ErrorMessage = "Firma adresi alanı boş bırakılamaz.")]
        public string FirmaAdresi { get; set; }

        [Required(ErrorMessage = "Şehir alanı boş bırakılamaz.")]
        public string Sehir { get; set; }

        [Required(ErrorMessage = "Vergi dairesi alanı boş bırakılamaz.")]
        public string VergiDairesi { get; set; }

        [Required(ErrorMessage = "Vergi numarası alanı boş bırakılamaz.")]
        public string VergiNumarasi { get; set; }

        [Required(ErrorMessage = "Açıklama ekleyiniz.")]
        public string Aciklama { get; set; }

        public DateTime BasvuruTarihi { get; set; } = DateTime.Now; // Başvuru Tarihi otomatik set edilecek
        public string BasvuruDurumu { get; set; } = "Beklemede"; // Başvuru Durumu varsayılan olarak "Beklemede"

        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false; // Başlangıçta okunmamış olacak
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Bildirimin oluşturulma zamanı
        public List<Notification> Notifications { get; set; }
        public List<BayiBasvuru> BayiBasvurular { get; set; }
    }
}
