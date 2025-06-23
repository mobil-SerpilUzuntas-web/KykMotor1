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

    public class EfCoreShippingDal : EfCoreGenericRepository<ShippingAddres,ShopContext>,IShippingAddressDal
    {
        public EfCoreShippingDal(ShopContext context) : base(context) { }

        public async Task<List<ShippingAddres>> GetAllShipping(string userId)
        {
            
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
            }

            // Veritabanında UserId'ye göre Buyer sorgusu
            return await _context.ShippingAddress
                  .Where(s => s.UserId == userId).ToListAsync();

        }

      
    }
}
