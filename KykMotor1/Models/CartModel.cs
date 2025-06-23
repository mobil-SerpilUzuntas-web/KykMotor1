using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{
    //buda sepet modeli
    public class CartModel
    {
        public int CartId{ get; set; }
        public List<CartItemModel> CartItems { get; set; }

        // Sepetteki ürünlerin KDV'siz toplam fiyatı
        public decimal TotalNormal()
        {
            return CartItems.Sum(item => item.NormalTotal());
        }

        // Sepetteki ürünlerin KDV dahil toplam fiyatı
        public decimal TotalKdv()
        {
            return CartItems.Sum(item => item.KdvToplamFiyat());
        }

        public decimal KargoDahil()
        {
            decimal kargo = 39;
            return CartItems.Sum(item => item.KdvToplamFiyat() + kargo);
        }

       
    }

    //Bu model Aslında SepetItem modeli
    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price  { get; set; }
        public int Quantity { get; set; }
        public List<Resim> Resims { get; set; }
       
  

        public decimal NormalTotal()
        {
            return Price * Quantity;
        }
        public decimal KdvToplamFiyat()
        {
            decimal kdvOrani = 0.18m; // KDV oranı
            return NormalTotal() * (1 +kdvOrani);
        }
       
    }


}
