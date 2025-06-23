using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface ICategoryServices
    {
        List<Category> tumKategorileriGetir();
        public void CetegoryDelete(int Id);
        Category GetById(int id);
        List<Category> GetAll();
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        // public List<Category> ListByFilter(Func<Category, bool> lamda);
        public List<Category> GetCategryBySubBir(int subcategoriId);

    }
}