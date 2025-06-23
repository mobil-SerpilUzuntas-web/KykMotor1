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
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
        public EfCoreCategoryDal(ShopContext context) : base(context)
        {
        }

        public List<Category> tumKategorileriGetir()
        {
            
                return _context.Categories
                    .Where(p => p.IsDeleted).ToList();
            
        }

        //public void CetegoryDelete(int Id)
        //{
        //    using (var context = new ShopContext())
        //    {
        //        var category = context.Categories.Find(Id);
        //        category.IsDeleted = false;
        //        context.SaveChanges();
        //    }
        //}
        public List<Category> GetCategryBySubBir(int subcategoriId)
        {
              return _context.Categories
                  .Include(s => s.SubbirCategories)
                    .Include(p => p.Products)
                 
                     .Where(s => s.Id == subcategoriId).ToList();
        }

        public List<Category> ListByFilter(Func<Category, bool> lamda)
        {
            
                var x = _context.Set<Category>().Where(lamda).ToList();
                return x;
        }

        public void CetegoryDelete(int Id)
        {
            // _context üzerinden işlemleri gerçekleştirin, yeni bir context oluşturmanıza gerek yok
            var category = _context.Categories.Find(Id);

            if (category != null)
            {
                // Eğer 'silme' işlemi için IsDeleted true yapılacaksa:
                category.IsDeleted = false;  // veya false, duruma göre ayarlayın.
                _context.SaveChanges();
            }
        }
    }

}




//return context.Products
//            .Include(r => r.Resims)

//            .Include(c => c.Category)
//            .ThenInclude(s => s.SubbirCategories)
//              .Where(s=>s.CategoryId == subcategoriId)  .ToList();