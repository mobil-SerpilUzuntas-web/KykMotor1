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
    public class BillingManager : IBillingServices
    {
        private IBillingDal _billingDal;
        public BillingManager(IBillingDal billingDal)
        {
            _billingDal = billingDal;
        }

        public void Create(BillingAddres entity)
        {
           _billingDal.Create(entity);
        }

        public async Task<BillingAddres> GetAllBilling(string userId)
        {
           return await _billingDal.GetAllBilling(userId);
        }
    }
}
