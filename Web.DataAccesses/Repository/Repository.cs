﻿using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Web.DataAccesses.Data;
using Web.DataAccesses.Repository.IRepository;
namespace Web.DataAccesses.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
        private readonly AppDbContext _appDbContext;
        internal DbSet<T> _dbSet;

        public Repository(AppDbContext appDbContext)
		{
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            return query.Any(filter);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var incProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incProp);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var incProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incProp);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}

