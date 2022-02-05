using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public interface IRepository<in TKey, T> where T : class
    {
        void SaveChanges();
        void Add(T entity);
        bool IsExist(Expression<Func<T, bool>> expression);
        T Get(TKey id);
        List<T> Get();
    }
}