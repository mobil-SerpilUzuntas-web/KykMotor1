using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class CartItem
    {
        public int Id { get; set; } // CartItem Id'si
        public int ProductId { get; set; } // Sepete eklenen ürünün Id'si
        public Product Product { get; set; } // Sepete eklenen ürünle ilişki
        public int CartId { get; set; } // Sepet Id'si (Foreign Key)
        public Cart Cart { get; set; } // Sepet ile ilişki
        public int Quantity { get; set; } // Sepete eklenen ürünün miktarı
    }
}
