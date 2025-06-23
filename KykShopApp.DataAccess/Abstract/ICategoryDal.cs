using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{
   
        public interface ICategoryDal : IRepository<Category>
        {
            List<Category> tumKategorileriGetir();
            public List<Category> GetCategryBySubBir(int subcategoriId);

             void CetegoryDelete(int Id);
    }
    
}
