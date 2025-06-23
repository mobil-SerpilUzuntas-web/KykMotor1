using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class Cart
    {
        public int Id { get; set; } // Sepet Id'si
        public string UserId { get; set; } // Kullanıcı Id'si (Foreign Key)
        public List<CartItem> CartItems { get; set; }
    }
}
