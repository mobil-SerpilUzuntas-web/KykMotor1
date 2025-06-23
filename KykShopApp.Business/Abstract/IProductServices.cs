using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface IProductServices
    {
        public void DeleteId(int id);
      
        public List<Product> GetSubCategoryByProduct(int Id);
        public List<Product> GetCategoryByProduct(int CategoryId);
       public Product GetById(int id);
        public  List<Product> GetAll();
        public List<Product> GetAllProductByCategory();
        public Product GetProductByIdWithDetails(int id);
        List<Product> GetPopulerProducts();
        //Bu metot GetProductsByCategory Home Index sayfasında Cetegory tıklayınca
        //CategoriBir actiona gelen ajax icin hazırlandı 
        public List<Product> GetProductsBySubBir(int subcategoriId);
        public List<Product> GetProductsBySubIki(int subIkiId);
        public List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetProductByCategory(int categoryId);
        List<Product> GetProductWithCategory();
        List<Product> UrunleriGetir();
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

    }
}
