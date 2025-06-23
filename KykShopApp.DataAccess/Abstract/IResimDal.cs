using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{
    public interface IResimDal :IRepository<Resim> 
    {

        public void DeleteId(int id);
        public List<Resim> GetResimByProductId(int id);
        public List<Resim> GetAllResimAndProduct();
        List<Resim> GetResimByCategoryId(int id);
        List<Resim> GetResimBySubikiCategoryId(int id);
      
    }
}

