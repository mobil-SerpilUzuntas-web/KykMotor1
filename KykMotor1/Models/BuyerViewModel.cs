using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{
    public class BuyerCartModel
    {
        public int CartId { get; set; }
        public List<BuyerCartItemModel> BuyerCartItems { get; set; }

        // Sepetteki ürünlerin KDV'siz toplam fiyatı
        public decimal TotalNormal()
        {
            return BuyerCartItems.Sum(item => item.NormalTotal());
        }

        // Sepetteki ürünlerin KDV dahil toplam fiyatı
        public decimal TotalKdv()
        {
            return BuyerCartItems.Sum(item => item.KdvToplamFiyat());
        }

        public decimal KargoDahil()
        {
            decimal kargo = 39;
            return BuyerCartItems.Sum(item => item.KdvToplamFiyat() + kargo);
        }


    }

    //Bu model Aslında SepetItem modeli
    public class BuyerCartItemModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<Resim> Resims { get; set; }
        public decimal NormalTotal()
        {
            return Price * Quantity;
        }
        public decimal KdvToplamFiyat()
        {
            decimal kdvOrani = 0.18m; // KDV oranı
            return NormalTotal() * (1 + kdvOrani);
        }
    }
    public class BuyerViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string UserId { get; set; }
        public List<ShippingAddres> ShippingAddress { get; set; }
        public List<Order> Orders { get; set; }
        public BillingAddres BillingAddres { get; set; }
        public BuyerCartModel BuyerCartModel { get; set; }
    }
}
