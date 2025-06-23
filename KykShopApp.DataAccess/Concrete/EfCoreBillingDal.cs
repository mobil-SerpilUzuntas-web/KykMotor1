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
    public class EfCoreBillingDal : EfCoreGenericRepository<BillingAddres,ShopContext> ,IBillingDal
    {
        public EfCoreBillingDal(ShopContext context) :base(context) {}

        public async Task<BillingAddres> GetAllBilling(string userId)
        {
            return await _context.BillingAddress
                 .FirstOrDefaultAsync(b => b.UserId == userId);
        }


        
    }
}
