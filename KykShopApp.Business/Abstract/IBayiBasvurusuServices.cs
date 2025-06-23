using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface IBayiBasvurusuServices
    {
        List<BayiBasvuru> GetAll();
        Task CreateAsync(BayiBasvuru entity);
        void Delete(BayiBasvuru entity);
        BayiBasvuru GetById(int id);
        void Update(BayiBasvuru entity);
    }
}
