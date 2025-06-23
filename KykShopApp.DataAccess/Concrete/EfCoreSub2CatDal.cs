using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Concrete
{

    
    public class EfCoreSub2CatDal : EfCoreGenericRepository<SubIkiCategory, ShopContext>, ISubIkiCatDal
    {

        public EfCoreSub2CatDal(ShopContext context) : base(context)
        {
        }
        public List<SubIkiCategory> tumAltIkiListe()
        {
            
                return _context.SubIkiCategories
                    .Where(s => s.IsDeleted).ToList();

            
        }

        public void DeleteId(int id)
        {
                var subbirCat = _context.SubIkiCategories.Find(id);
                if (subbirCat != null)
                {
                    subbirCat.IsDeleted = false;
                //context.SubIkiCategories.Remove(subbirCat);
                _context.SaveChanges();
                }
            
        }
            
            public List<SubIkiCategory> GetAllBySubBirId(int subBirId)
           {
           
              return _context.SubIkiCategories.Where(s=>s.SubBirId == subBirId).ToList();
            
          
           }
        public List<SubIkiCategory> GetSub2CategoriesBySubbirId(int subbirId)
        {
            
                return GetAll().Where(s2 => s2.Id == subbirId).ToList();
            
        }

        public List<SubIkiCategory> GetSub2FilterCategory(int subbirId)
        {
            
                var sub2Cat = _context.SubIkiCategories.Where(s => s.SubBirId == subbirId).ToList();
                return sub2Cat.ToList();

            
        }
        public List<SubIkiCategory> ListByFilter(Func<SubIkiCategory, bool> lamda)
        {
            throw new NotImplementedException();
        }

        


      
    }
}
