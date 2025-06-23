using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{
    public interface IProductDal : IRepository<Product>
    {
        public void DeleteId(int id);
         List<Product> GetSubCategoryByProduct(int Id);
         List<Product> GetCategoryByProduct(int CategoryId);
         List<Product> GetAllProductByCategory();
        List<Product> GetProductByCategory(int categoryId);
        List<Product> GetProductWithCategory();
        List<Product> GetAllProductsWithCategoryAndImages();
        List<Product> UrunleriGetir(); 
        Product GetProductByIdWithDetails(int id);
        List<Product> GetProductsBySubIki(int subIkiId);
        List<Product> GetProductsBySubBir(int subcategoriId);
        List<Product> GetProductsByCategory(int categoryId);


    }
}
