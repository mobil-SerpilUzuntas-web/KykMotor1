using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface IResimServices
    {
        
        public void DeleteId(int id);
        Resim GetById(int id);
        List<Resim> GetAll();
        List<Resim> GetAllResimAndProduct();
        public List<Resim> GetResimByProductId(int id);
        void Create(Resim entity);
        void Update(Resim entity);
        void Delete(Resim entity);
        List<Resim> GetResimByCategoryId(int id);
        public List<Resim> GetResimBySubikiCategoryId(int Id);
    }
}
