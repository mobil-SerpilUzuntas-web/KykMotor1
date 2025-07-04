﻿using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{

    public interface IRepository<T> where T : class
    {
     
        T GetById(int id);
        T GetOne(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        Task CreateAsync(T entity);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
