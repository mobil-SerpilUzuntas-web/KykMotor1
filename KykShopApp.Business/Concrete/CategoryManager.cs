using KykShopApp.Business.Abstract;
using KykShopApp.DataAccess.Abstract;
using KykShopApp.DataAccess.Concrete;
using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Concrete
{
    public class CategoryManager : ICategoryServices

    {

        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;

        }
        public List<Category> tumKategorileriGetir()
        {
            return _categoryDal.tumKategorileriGetir();
        }
        public void CetegoryDelete(int Id)
        {
            _categoryDal.CetegoryDelete(Id);
        }
        public void Create(Category entity)
        {
            _categoryDal.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryDal.Delete(entity);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll().ToList();
        }

        public Category GetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public List<Category> GetCategryBySubBir(int subcategoriId)
        {
            return _categoryDal.GetCategryBySubBir(subcategoriId);
        }
        //GetCategryBySubBir
        //public List<Category> ListByFilter(Func<Category, bool> lamda)
        //{
        //    return _categoryDal.ListByFilter(lamda);
        //}

        public void Update(Category entity)
        {
            _categoryDal.Update(entity);
        }
    }
}
