using KykShopApp.Business.Abstract;
using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Concrete
{
    public class BayiBasvuruManager : IBayiBasvurusuServices
    {
        private IBayiBasvuruDal _bayiBasvuruDal;
        public BayiBasvuruManager(IBayiBasvuruDal bayiBasvuruDal)
        {
            _bayiBasvuruDal = bayiBasvuruDal;

        }

        public async Task CreateAsync(BayiBasvuru entity)
        {
            await _bayiBasvuruDal.CreateAsync(entity);
        }

        public void Delete(BayiBasvuru entity)
        {
            _bayiBasvuruDal.Delete(entity);
        }

        public List<BayiBasvuru> GetAll()
        {
            return _bayiBasvuruDal.GetAll();

        }

        public BayiBasvuru GetById(int id)
        {
            return _bayiBasvuruDal.GetById(id);
        }

        public void Update(BayiBasvuru entity)
        {
            _bayiBasvuruDal.Update(entity);
        }
    }
}
