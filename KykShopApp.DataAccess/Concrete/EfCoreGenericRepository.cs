
using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KykShopApp.DataAccess.Concrete
{
    public class EfCoreGenericRepository<T, TContext> : IRepository<T>
        where T : class
        where TContext : DbContext
    {
        protected readonly TContext _context;

        // Bağımlılık enjeksiyonu ile context'i alıyoruz
        public EfCoreGenericRepository(TContext context)
        {
            _context = context;
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

       
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null
                ? _context.Set<T>().ToList()
                : _context.Set<T>().Where(filter).ToList();
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

       
        public virtual T GetOne(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefault(filter);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}


//using KykShopApp.DataAccess.Abstract;
//using KykShopApp.Entities;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace KykShopApp.DataAccess.Concrete
//{
//    public class EfCoreGenericRepository<T, TContext> : IRepository<T>
//     where T : class
//     where TContext : ShopContext, new()
//    {
//        public void Create(T entity)
//        {
//            using (var context = new TContext())
//            {
//                context.Set<T>().Add(entity);
//                context.SaveChanges();
//            }
//        }

//        public void Delete(T entity)
//        {
//            using (var context = new TContext())
//            {
//                context.Set<T>().Remove(entity);
//                context.SaveChanges();
//            }
//        }

//        public List<T> GetAll(Expression<Func<T, bool>>? filter = null)
//        {
//            using (var context = new TContext())
//            {
//                return filter == null
//                     ? context.Set<T>().ToList()
//                     : context.Set<T>().Where(filter).ToList();
//            }
//        }

//        public T GetById(int id)
//        {
//            using (var context = new TContext())
//            {
//                return context.Set<T>().Find(id);


//            }
//        }

//        public T GetOne(Expression<Func<T, bool>> filter)
//        {
//            using (var context = new TContext())
//            {
//                return filter == null
//                    ? context.Set<T>().FirstOrDefault()
//                    : context.Set<T>().FirstOrDefault(filter);

//            }
//        }

//        public void Update(T entity)
//        {
//            using (var context = new TContext())
//            {
//                context.Entry(entity).State = EntityState.Modified;
//                context.SaveChanges();

//            }
//        }
//    }
//}
