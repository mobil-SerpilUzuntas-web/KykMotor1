using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{
    public interface IBuyerDal : IRepository<Buyerr>
    {
        // Buyer'ı userId'ye göre getir
        Buyerr GetBuyerByUserId(string userId);

        Task<List<Buyerr>> GetAllBuyerList(string userId);
        Task<Buyerr> GetBuyerSonKayit(string userId);
        Task<Buyerr> GetByList(string userId);
        int getBuyerId(string userId);
    }
}
