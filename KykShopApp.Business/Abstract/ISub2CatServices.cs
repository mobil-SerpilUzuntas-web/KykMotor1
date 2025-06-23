using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface ISub2CatServices
    {
        List<SubIkiCategory> tumAltIkiListe();
        public void DeleteId(int id);
        public List<SubIkiCategory> GetAllBySubBirId(int subBirId);
        SubIkiCategory GetById(int id);
        List<SubIkiCategory> GetAll();
        public List<SubIkiCategory> GetSub2FilterCategory(int subbirId);
        public List<SubIkiCategory> GetSub2CategoriesBySubbirId(int subbirId);
        //List<Sub2Cat> ListByFilter(Func<Sub2Cat, bool> lamda);
        void Create(SubIkiCategory entity);
        void Update(SubIkiCategory entity);
        void Delete(SubIkiCategory entity);
      
       

    }
}
