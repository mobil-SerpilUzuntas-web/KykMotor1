using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface ISubbirServices
    {
        public List<SubbirCategori> tumAltBirListe();
        public void DeleteId(int id);
        SubbirCategori GetById(int id);
        List<SubbirCategori> GetAll();
        List<SubbirCategori> GetSubbirCategoriesByCategoryId(int categoryId);
        public List<SubbirCategori> ListByFilter(int categoryId);
        void Create(SubbirCategori entity);
        void Update(SubbirCategori entity);
        void Delete(SubbirCategori entity);
       

    }
}
