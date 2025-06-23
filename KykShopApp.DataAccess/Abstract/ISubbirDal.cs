using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{
    public interface ISubbirDal : IRepository<SubbirCategori>
    {
        List<SubbirCategori> tumAltBirListe();
        public void DeleteId(int id);
        List<SubbirCategori> GetSubbirCategoriesByCategoryId(int categoryId);

        public List<SubbirCategori> ListByFilter(int categoryId);

    }
}
