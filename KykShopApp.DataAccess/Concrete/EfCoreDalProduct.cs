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
    public class EfCoreDalProduct : EfCoreGenericRepository<Product, ShopContext>, IProductDal
          
    {
        public EfCoreDalProduct(ShopContext context) : base(context)
        {
        }

        public void DeleteId(int id)
        {
            
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    //context.Products.Remove(product);
                    product.IsDeleted= false;
                   _context.SaveChanges();
                }
            
        }
    
        public List<Product> GetSubCategoryByProduct(int Id)
        {
            
                return _context.Products
                    .Where(su => su.SubIkiId == Id).ToList();
          
        }
        public List<Product> GetCategoryByProduct(int CategoryId)
        {
           
                   return _context.Products
                    .Where(p => p.CategoryId == CategoryId).ToList();
            
        }
       
        public List<Product> GetAllProductByCategory()
        {
           
                return _context.Products
                           .Include(p => p.Category)
                           .Include(p => p.SubbirCategori)
                           .Include(p => p.SubIkiCategory)
                            .Where(p => p.IsDeleted)  // Silinmemiş ürünleri filtrele
                            .ToList();
                //Where Komutu Silinmemiş ürünleri getirir eger silinmiş ürünleri de getirmek istiyorsan bu kod blogunu kapat.
                //.Where(p => p.IsDeleted &&
                //     p.Category.IsDeleted &&
                //     p.SubbirCategori.IsDeleted &&
                //     p.SubIkiCategory.IsDeleted)
                //.ToList();  // Hem Product hem de ilişkili verilerde silinmemiş kayıtları filtreler

            
        }

        public List<Product> UrunleriGetir()
        {
            
                return _context.Products
                    .Include(r => r.Resims).ToList();
            
        }

        public List<Product> GetProductWithCategory()
        {
           
                return _context.Products
                    .Include(p => p.Category).ToList();


            

        }


        public List<Product> GetAllProductsWithCategoryAndImages()
        {
            throw new NotImplementedException();
        }


        public Product GetProductByIdWithDetails(int id)
        {
            

                var product = _context.Products
                   .Include(p => p.Resims)
                   .Include(p => p.Category)
                    .ThenInclude(s => s.SubbirCategories)
                    .ThenInclude(su => su.SubIkiCategories)
                    .FirstOrDefault(p => p.Id == id);
                return product;

          

        }

        //bURAYA GELİYOR
        public List<Product> GetProductByCategory(int categoryId)
        {

           
                var product = _context.Products
                     .Include(p => p.Resims)
                     .Include(c => c.Category)
                     .ThenInclude(s => s.SubbirCategories)
                     .Where(su => su.CategoryId == categoryId).ToList();
                return product.ToList();
            
        }
    

        public List<Product> GetProductsByCategory(int categoryId)
        {
         
                return _context.Products
               .Include(p => p.Resims)
               .Include(c => c.Category)// Ürünlerle birlikte resimleri de dahil ediyoruz
               .Where(p => p.CategoryId == categoryId)  // Verilen kategori ID'sine göre filtreliyoruz
                .ToList();

            

        }
        public List<Product> GetProductsBySubBir(int subBirId)
        {
            
                var product = _context.Products
                .Include(r => r.Resims)
                .Include(c => c.Category)
                .ThenInclude(c => c.SubbirCategories)
                .Where(s => s.SubBirId == subBirId).ToList();
                return product.ToList();
            
        }

        public List<Product> GetProductsBySubIki(int subIkiId)
        {

            
                var product = _context.Products
                    .Include(r => r.Resims)
                    .Include(c => c.Category)
                    .ThenInclude(s => s.SubbirCategories)
                    .ThenInclude(su => su.SubIkiCategories)
                    .Where(c => c.SubIkiId == subIkiId).ToList();
                return product.ToList();
            
        }
    }
}
