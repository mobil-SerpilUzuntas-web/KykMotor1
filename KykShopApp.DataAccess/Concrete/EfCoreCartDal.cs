using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Concrete
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart,ShopContext> ,ICartDal
    {
        public EfCoreCartDal(ShopContext context) : base(context)
        {
        }
     

            public void CartItemsDelete(int cartId)
        {
            // İlk olarak, belirtilen cartId'ye sahip sepeti ve ilişkili CartItems'ları alıyoruz
            var cart = _context.Carts
                .Include(c => c.CartItems) 
                .FirstOrDefault(c => c.Id == cartId);

            // Eğer sepet ve ilişkili CartItems mevcutsa, ilişkili CartItems listesini temizliyoruz
            if (cart != null && cart.CartItems != null)
            {
                cart.CartItems.Clear();// CartItems listesindeki tüm öğeleri temizler
                _context.SaveChanges();
            }
        }
      
            public async Task<Cart> GetByUserId(string userId)
             {
               return await _context.Carts
                         .Include(c => c.CartItems)
                         .ThenInclude(p => p.Product)     
                         .ThenInclude(p => p.Resims)
                         .FirstOrDefaultAsync(i => i.UserId == userId);
             }
            public override void Update(Cart entity)
            {
            _context.Update(entity);
            _context.SaveChanges();
           }

        public void DeleteFromCart(int cartId, int cartItemId)
        {
            // Cart'ı bul
            var cart = _context.Carts
                .Include(c => c.CartItems) // Cart ile birlikte CartItems da yüklensin
                .FirstOrDefault(c => c.Id == cartId);

            if (cart != null)
            {
                // CartItems koleksiyonundan cartItemId'ye sahip olanı bul
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

                if (cartItem != null)
                {
                    // CartItems'dan ilgili itemi sil
                    cart.CartItems.Remove(cartItem);

                    // Değişiklikleri veritabanına kaydet
                    _context.SaveChanges();
                }
            }
        }

        public int GetQuantityByUserId(string userId)
        {
            return _context.Carts
                       .Where(c => c.UserId == userId) // Kullanıcının sepetini al
                       .SelectMany(c => c.CartItems) // Sepetteki ürünleri al
                       .Sum(ci => ci.Quantity); // Toplam ürün miktarını hesapla
        }

        public Task<Cart> GetByUserTextId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
