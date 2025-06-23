using KykShopApp.Business.Abstract;
using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Concrete
{
    public class BuyerManager : IBuyerServices
    {
        private IBuyerDal _buyerDal;
        public BuyerManager(IBuyerDal buyerDal)
        {
            _buyerDal = buyerDal;
        }

        public int getBuyerId(string userId)
        {
            return _buyerDal.getBuyerId(userId);
        }
        // Buyer'ı al veya oluştur
        public Buyerr GetOrCreateBuyerByUserId(string userId)
        {
            var existingBuyer = _buyerDal.GetBuyerByUserId(userId);
            if (existingBuyer == null)
            {
                return null; // Eğer Buyer yoksa null döndür
            }
            return existingBuyer; // Buyer varsa geri döndür
        }

  
        public async Task<Buyerr> GetBuyer(string userId)
        {
            return await _buyerDal.GetByList(userId);

        }
        public async Task<Buyerr> GetBuyerSonKayit(string userId)
        {
            return await _buyerDal.GetBuyerSonKayit(userId);

        }
        public void AddBuyer(Buyerr buyer)
        {
            // Gerekli bilgiler set ediliyor
            if (buyer.RegistrationDate == default)
                buyer.RegistrationDate = DateTime.Now;

            if (buyer.LastLoginDate == default)
                buyer.LastLoginDate = DateTime.Now;

            if (string.IsNullOrEmpty(buyer.UserId))
                throw new ArgumentException("User ID boş olamaz.");

            // Veritabanına kayıt
            _buyerDal.Create(buyer);
        }

        public void Create(Buyerr entity)
        {
            _buyerDal.Create(entity);
        }

        public async Task<List<Buyerr>> GetAllBuyer(string userId)
        {
            return await _buyerDal.GetAllBuyerList(userId);

        }

      
    }
}
