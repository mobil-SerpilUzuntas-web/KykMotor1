using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface IBuyerServices
    {
        int getBuyerId(string userId);
        // Buyer'ı al veya oluştur
        Buyerr GetOrCreateBuyerByUserId(string userId);
        void Create(Buyerr entity);

        Task<List<Buyerr>> GetAllBuyer(string userId);
        Task<Buyerr> GetBuyer(string userId);
        void AddBuyer(Buyerr buyer);

        Task<Buyerr> GetBuyerSonKayit(string userId);
    }
}
