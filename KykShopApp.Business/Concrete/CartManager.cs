using KykShopApp.Business.Abstract;
using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Concrete
{
    public class CartManager : ICartServices
    {
        private ICartDal _cartDal;

        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
                 var cart = await GetCartByUserAsync(userId);
            //burayı aldım
                 if (cart != null)
                 {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }
                _cartDal.Update(cart);
            }
        }

     
  
        //önce buradan alıyordu
        //Cart Conturoller de Index Metoduna CartItems Tablosunu getiriyor 
        public async Task<Cart> GetCartByUserAsync(string userId)
        {
            return await _cartDal.GetByUserId(userId);

        }
        public void CartItemsDelete(int Id)
        {
            _cartDal.CartItemsDelete(Id);
        }

        public async Task DeleteFromCart(string userId, int cartItemId)
        {
            var cart = await GetCartByUserAsync(userId);
            if (cart != null)
            {
                var cartId=cart.Id;
                _cartDal.DeleteFromCart(cartId, cartItemId);
            }
        }

    

        public int GetQuantityByUserId(string userId)
        {
            return _cartDal.GetQuantityByUserId(userId);
        }



        //Burda yeni bir Cart nesnesi oluşturuyoruz parametre UserId alan
        public void IntializeCart(string userId)
        {
            _cartDal.Create(new Cart() { UserId = userId });
        }
     

       



      


    }
}
