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
    public class Sub2CatManager : ISub2CatServices
    {
        private readonly ISubIkiCatDal _subIkiCatDal;
        public Sub2CatManager(ISubIkiCatDal sub2CatDal)
        {
            _subIkiCatDal = sub2CatDal;
        }

       
        public void DeleteId(int id)
        {
            _subIkiCatDal.DeleteId(id);
        }
        public List<SubIkiCategory> GetAllBySubBirId(int subBirId)
        {
           return _subIkiCatDal.GetAllBySubBirId(subBirId);
        }
        public void Create(SubIkiCategory entity)
        {
            _subIkiCatDal.Create(entity);
        }

        public void Delete(SubIkiCategory entity)
        {
            _subIkiCatDal.Delete(entity);
        }

        public List<SubIkiCategory> GetAll()
        {
            return _subIkiCatDal.GetAll().ToList();
        }

        public SubIkiCategory GetById(int id)
        {
            return _subIkiCatDal.GetById(id);
        }

        public void Update(SubIkiCategory entity)
        {
            _subIkiCatDal.Update(entity);
        }

        public List<SubIkiCategory> GetSub2CategoriesBySubbirId(int subbirId)
        {
            return _subIkiCatDal.GetSub2CategoriesBySubbirId(subbirId).ToList();
        }

        public List<SubIkiCategory> GetSub2FilterCategory(int subbirId)
        {
            return _subIkiCatDal.GetSub2FilterCategory(subbirId);
        }

        public List<SubIkiCategory> tumAltIkiListe()
        {
           return _subIkiCatDal.tumAltIkiListe();
        }

        //public List<Sub2Cat> ListByFilter(Func<Sub2Cat, bool> lamda)
        //{
        //    return _sub2CatDal.ListByFilter(lamda);
        //}
    }
}