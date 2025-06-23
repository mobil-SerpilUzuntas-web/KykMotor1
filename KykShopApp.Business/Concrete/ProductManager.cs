using KykShopApp.Business.Abstract;
using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Concrete
{
    public class ProductManager : IProductServices
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetSubCategoryByProduct(int Id)
        {
            return _productDal.GetSubCategoryByProduct(Id);
        }
        public void DeleteId(int id)
        {
            _productDal.DeleteId(id);
        }
        public List<Product> GetCategoryByProduct(int CategoryId)
        {
            return _productDal.GetCategoryByProduct(CategoryId);
        }
        public List<Product> GetAllProductByCategory()
        {
            return _productDal.GetAllProductByCategory();
        }
        public void Create(Product entity)
        {
            _productDal.Create(entity);
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetAllProductsWithCategoryAndImages()
        {
            return _productDal.GetAllProductsWithCategoryAndImages();
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public List<Product> GetPopulerProducts()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            return _productDal.GetProductByCategory(categoryId);
        }

        public Product GetProductByIdWithDetails(int id)
        {
            return _productDal.GetProductByIdWithDetails(id);
        }

        //Buraya Geliyor
        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDal.GetProductByCategory(categoryId).ToList();
        }

        public List<Product> GetProductsBySubBir(int subcategoriId)
        {
            return _productDal.GetProductsBySubBir(subcategoriId);
        }

        public List<Product> GetProductsBySubIki(int subIkiId)
        {
            return _productDal.GetProductsBySubIki(subIkiId);
        }

        public List<Product> GetProductWithCategory()
        {
            return _productDal.GetProductWithCategory();

        }
        public void Update(Product entity)
        {
            _productDal.Update(entity);
        }

        public List<Product> UrunleriGetir()
        {
            return _productDal.UrunleriGetir();

        }
    }
}
