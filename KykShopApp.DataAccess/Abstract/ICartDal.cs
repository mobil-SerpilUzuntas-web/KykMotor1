using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{
    public interface ICartDal : IRepository<Cart>
    {
        void CartItemsDelete(int id);
        void DeleteFromCart(int cartId, int cartItemId);

        //Cart Conturoller de Index Metoduna CartItems Tablosunu getiriyor GetCartByUserAsync gönderiliyor
        Task<Cart> GetByUserId(string userId);
        Task<Cart> GetByUserTextId(string userId);
        int GetQuantityByUserId(string userId);
    }
}
