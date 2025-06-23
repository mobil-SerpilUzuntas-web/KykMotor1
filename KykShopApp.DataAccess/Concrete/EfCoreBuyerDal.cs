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
    public class EfCoreBuyerDal : EfCoreGenericRepository<Buyerr, ShopContext>, IBuyerDal
    {
        public EfCoreBuyerDal(ShopContext context) : base(context)
        {
        }

        public int getBuyerId(string userId)
        {
            var buyer = _context.Buyerrs.FirstOrDefault(b => b.UserId == userId);

            if (buyer == null)
            {
                throw new ArgumentException("Kullanıcıya ait Buyer bulunamadı.");
            }

            return buyer.Id; // BuyerId döndürülüyor

        }
        // Buyer'ı userId'ye göre getir
        public Buyerr GetBuyerByUserId(string userId)
        {
            return _context.Buyerrs.FirstOrDefault(b => b.UserId == userId); // userId'ye göre Buyer'ı getir
        }

        //Yeni bir Buyer ekle
        //public void Add(Buyerr buyer)
        //{
        //    _context.Buyerrs.Add(buyer); // Yeni Buyer'ı ekle
        //    _context.SaveChanges(); // Değişiklikleri kaydet
        //}
        public async Task<List<Buyerr>> GetAllBuyerList(string userId)
        {
               return await _context.Buyerrs
                .Where(x => x.UserId == userId)
               .ToListAsync();

        }

        //        Alternatif Yöntem: Max ile Son Id'yi Bulma
        //Eğer OrderByDescending kullanmak yerine, doğrudan Id'yi bulmak isterseniz, aşağıdaki yöntem kullanılabilir:

        //public async Task<Buyerr> GetBuyerSonKayıtGetir(string userId)
        //{
        //    int? maxId = await _context.Buyerrs
        //        .Where(b => b.UserId == userId)
        //        .MaxAsync(b => (int?)b.Id); // Max Id'yi al

        //    if (maxId == null)
        //        return null;

        //    return await _context.Buyerrs.FirstOrDefaultAsync(b => b.Id == maxId.Value);
        //}

        //Tarihe göre en son eklenen tarihe göre listeler
        //public async Task<Buyerr> GetBuyerSonKayıtTarih(string userId)
        //{
        //    return await _context.Buyers
        //        .Where(b => b.UserId == userId)         // Filtreleme: userId'ye göre
        //        .OrderByDescending(b => b.CreatedDate) // Tarihe göre azalan sıralama
        //        .FirstOrDefaultAsync();                // İlk (en yeni) kaydı al
        //}

        public async Task<Buyerr> GetBuyerSonKayit(string userId)
           {
               return await _context.Buyerrs
              .Where(b => b.UserId == userId)         // Filtreleme: userId'ye göre
              .OrderByDescending(b => b.Id)          // Id'ye göre azalan sırala
              .FirstOrDefaultAsync();                // En yüksek Id'yi al (son eklenen)
           }


        public async Task<Buyerr> GetByList(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
            }

            // Veritabanında UserId'ye göre Buyer sorgusu
            return await _context.Buyerrs
                .FirstOrDefaultAsync(b => b.UserId == userId);
        }

     
    }
}
