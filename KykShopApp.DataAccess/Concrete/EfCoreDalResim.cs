using KykShopApp.DataAccess.Abstract;
using KykShopApp.DataAccess.Concrete;
using KykShopApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Concrete
{
    public class EfCoreDalResim : EfCoreGenericRepository<Resim, ShopContext>, IResimDal
    {
        public EfCoreDalResim(ShopContext context) : base(context)
        {
        }
      
        public List<Resim> GetResimBySubikiCategoryId(int productId)
        {
            // Önce productId ile ürünü bul
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return new List<Resim>();

            // Ürünün SubİkiCategoryId’sini al
            int subikiCategoryId = (int)product.SubIkiId;

            // Aynı SubİkiCategoryId’ye sahip tüm ürünlerin ID'lerini al
            var productIds = _context.Products
                .Where(p => p.SubIkiId == subikiCategoryId)
                .Select(p => p.Id)
                .ToList();  // Veritabanından çekiyoruz (client-side işlemi için)

            // Tüm resimleri çek (client tarafında işlem yapacağız)
            var resimler = _context.Resims
                .Where(r => productIds.Contains(r.ProductId) && r.IsDeleted==true)
                .ToList() // Verileri veritabanından çekiyoruz
                .GroupBy(r => r.ProductId) // Client tarafında gruplama yapıyoruz
                .SelectMany(g => g.OrderBy(r => r.Id).Take(3)) // İlk 2 resmi al
                .ToList();

            return resimler;
        }


     

  
        public void DeleteId(int id)
        {
            
                var image = _context.Resims.Find(id);
                if (image != null)
                {
                    image.IsDeleted = false;  // Soft delete:     Veriyi silmek yerine işaretleme                   //context.Resims.Remove(image);
                   _context.SaveChanges();
                
                }
        }
        public List<Resim> GetAllResimAndProduct()
        {

            return _context.Resims
                .Include(p => p.Product)
                .Where(p => p.ProductId ==p.Id).ToList();

        }
        public List<Resim> GetAllResimAndProduct(int productId)
        {

            return _context.Resims
                .Include(p => p.Product)
                .Where(p => p.ProductId == productId).ToList();

        }
       

        public List<Resim> GetResimByProductId(int id)
        {

            return _context.Resims
                .Where(r => r.ProductId == id && r.IsDeleted == true).ToList();

        }
        //Bu metodu hazırla
        public List<Resim> GetResimByCategoryId(int id)
        {
                        return _context.Resims
                       .Where(r => r.ProductId == r.ProductId)
                         .ToList();
        }
     

    }
}


