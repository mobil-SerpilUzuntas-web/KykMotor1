using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface IShippingServices
    {
   
       Task<List<ShippingAddres>> GetAllShipping(string userId);
       void Create(ShippingAddres entity);
    }
}
