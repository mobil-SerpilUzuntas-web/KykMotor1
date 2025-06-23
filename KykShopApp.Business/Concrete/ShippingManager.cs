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
    public class ShippingManager : IShippingServices
    {
        private IShippingAddressDal _shippingAddressDal;
        public ShippingManager(IShippingAddressDal shippingAddressDal)
        {
            _shippingAddressDal = shippingAddressDal;
        }

        public void Create(ShippingAddres entity)
        {
            _shippingAddressDal.Create(entity); 
        }

        public async Task<List<ShippingAddres>> GetAllShipping(string userId)
        {
            return await _shippingAddressDal.GetAllShipping(userId);
                 
        }
    }
}
