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
    public class ResimManager : IResimServices
    {
        private IResimDal _resimDal;
        public ResimManager(IResimDal resimDal)
        {
            _resimDal = resimDal;
        }

        public void DeleteId(int id)
        {
            _resimDal.DeleteId(id);
        }
        public void Create(Resim entity)
        {
            _resimDal.Create(entity);
        }

        public void Delete(Resim entity)
        {
             _resimDal.Delete(entity);
        }

        public List<Resim> GetAll()
        {
            return _resimDal.GetAll();

        }

        public List<Resim> GetAllResimAndProduct()
        {
            return _resimDal.GetAllResimAndProduct();
        }

        public Resim GetById(int id)
        {
            return _resimDal.GetById(id);
        }
       
        public List<Resim> GetResimByProductId(int id)
        {
           return _resimDal.GetResimByProductId(id);
        }

        public void Update(Resim entity)
        {
           _resimDal.Update(entity);
        }

        public List<Resim> GetResimByCategoryId(int id)
        {
            return _resimDal.GetResimByCategoryId(id);
        }

        public List<Resim> GetResimBySubikiCategoryId(int Id)
        {
            return _resimDal.GetResimBySubikiCategoryId(Id);
        }
    }
}

