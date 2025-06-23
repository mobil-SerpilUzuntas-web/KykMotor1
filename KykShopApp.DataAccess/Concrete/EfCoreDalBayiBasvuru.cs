using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Concrete
{
    public class EfCoreDalBayiBasvuru : EfCoreGenericRepository<BayiBasvuru, ShopContext>, IBayiBasvuruDal
    {
        public EfCoreDalBayiBasvuru(ShopContext context) : base(context)
        {
        }
    }
}
