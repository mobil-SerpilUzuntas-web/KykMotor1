using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{
    public interface ISubIkiCatDal : IRepository<SubIkiCategory>
    {
        List<SubIkiCategory> tumAltIkiListe();
        public void DeleteId(int id);
        public List<SubIkiCategory> GetAllBySubBirId(int subBirId);
        public List<SubIkiCategory> GetSub2CategoriesBySubbirId(int subbirId);
        public List<SubIkiCategory> GetSub2FilterCategory(int subbirId);
    }

}
