//using KykShopApp.DataAccess.Abstract;
//using KykShopApp.Entities;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;

//namespace KykShopApp.DataAccess.Concrete
//{
//    public class EfCoreSubBirDal : EfCoreGenericRepository<SubbirCategori, ShopContext>, ISubbirDal
//    {
//        public EfCoreSubBirDal(ShopContext context) : base(context)
//        {
//        }

//        public List<SubbirCategori> TumAltBirListe()
//        {
//            // EfCoreGenericRepository'de Dependency Injection kullanıldığı için _context kullanılmalı
//            return _context.SubbirCategories
//                .Where(s => s.IsDeleted)
//                .ToList();
//        }

//        public void DeleteId(int id)
//        {
//            var subbirCat = _context.SubbirCategories.Find(id);
//            if (subbirCat != null)
//            {
//                subbirCat.IsDeleted = false;
//                _context.SaveChanges();
//            }
//        }

//        public List<SubbirCategori> GetSubbirCategoriesByCategoryId(int categoryId)
//        {
//            return GetAll().Where(sb => sb.CategoryId == categoryId).ToList();
//        }

//        public List<SubbirCategori> ListByFilter(int categoryId)
//        {
//            return _context.SubbirCategories
//                .Where(s => s.CategoryId == categoryId)
//                .ToList();
//        }

//        public List<SubbirCategori> tumAltBirListe()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}



using KykShopApp.DataAccess.Abstract;
using KykShopApp.DataAccess.Concrete;
using KykShopApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Concrete
{
    public class EfCoreSubBirDal : EfCoreGenericRepository<SubbirCategori, ShopContext>, ISubbirDal
    {

        public EfCoreSubBirDal(ShopContext context) : base(context)
        {
        }

        public List<SubbirCategori> tumAltBirListe()
        {

            return _context.SubbirCategories
                .Where(s => s.IsDeleted).ToList();


        }
        public void DeleteId(int id)
        {

            var subbirCat = _context.SubbirCategories.Find(id);
            if (subbirCat != null)
            {
                //context.SubbirCategories.Remove(subbirCat);
                subbirCat.IsDeleted = false;
                _context.SaveChanges();
            }

        }
        public List<SubbirCategori> GetSubbirCategoriesByCategoryId(int categoryId)
        {

            return GetAll().Where(sb => sb.CategoryId == categoryId).ToList();

        }


        public List<SubbirCategori> ListByFilter(int categoryId)
        {

            var x = _context.SubbirCategories.Where(s => s.CategoryId == categoryId).ToList();
            return x;


        }
    } }



