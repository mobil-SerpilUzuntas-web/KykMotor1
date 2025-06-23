using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Concrete
{
    public class EfCoreOrderDal : EfCoreGenericRepository<Order,ShopContext> ,IOrderDal
    {
        public EfCoreOrderDal(ShopContext context) : base(context) 
        { 

        }

    }  
}
