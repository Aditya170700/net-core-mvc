using System;
using System.Linq.Expressions;

namespace Web.DataAccesses.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll(string? includeProperties = null);
		T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
		bool Any(Expression<Func<T, bool>> filter);
        void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
    }
}

