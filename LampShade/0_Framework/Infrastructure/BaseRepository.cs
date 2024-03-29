﻿using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public class BaseRepository<TKey, T> : IRepository<TKey, T> where T : BaseEntity<TKey>
    {
        #region Constructor

        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        #endregion

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public bool IsExist(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public T Get(TKey id)
        {
            return _context.Find<T>(id);
        }

        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }
    }
}