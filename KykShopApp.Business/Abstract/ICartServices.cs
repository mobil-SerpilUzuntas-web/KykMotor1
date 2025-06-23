using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    
        public interface ICartServices
        {
        void IntializeCart(string userId);    
         Task<Cart> GetCartByUserAsync(string userId);
         Task AddToCartAsync(string userId, int productId, int quantity);
         Task DeleteFromCart(string userId, int cartItemId);
         void CartItemsDelete(int Id);
         int GetQuantityByUserId(string userId);
     


    }


}
   