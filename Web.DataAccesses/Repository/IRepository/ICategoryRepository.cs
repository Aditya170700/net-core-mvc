using System;
using Web.Models;

namespace Web.DataAccesses.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Save();
		void Update(Category entity);
	}
}

