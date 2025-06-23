using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface IBillingServices
    {
        void Create(BillingAddres entity);
      Task<BillingAddres> GetAllBilling(string userId);
       
    }
}
