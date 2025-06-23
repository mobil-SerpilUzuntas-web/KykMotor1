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
    public class SubbirManager : ISubbirServices
    {
        private readonly ISubbirDal _subbirDal;
        public SubbirManager(ISubbirDal subbirDal)
        {
            _subbirDal = subbirDal;

        }

        public void DeleteId(int id)
        {
            _subbirDal.DeleteId(id);
        }
        public void Create(SubbirCategori entity)
        {
            _subbirDal.Create(entity);
        }

        public void Delete(SubbirCategori entity)
        {
            _subbirDal.Delete(entity);
        }

        public List<SubbirCategori> GetAll()
        {
            return _subbirDal.GetAll().ToList();
        }

        public SubbirCategori GetById(int id)
        {
            return _subbirDal.GetById(id);
        }

        public void Update(SubbirCategori entity)
        {
            _subbirDal.Update(entity);
        }

        public List<SubbirCategori> GetSubbirCategoriesByCategoryId(int categoryId)
        {
            return _subbirDal.GetSubbirCategoriesByCategoryId(categoryId).ToList();
        }

        public List<SubbirCategori> ListByFilter(int categoryId)
        {
            return _subbirDal.ListByFilter(categoryId).ToList();
        }

        public List<SubbirCategori> tumAltBirListe()
        {
            return _subbirDal.tumAltBirListe();
        }
    }
}
