using System;
using Web.Models;

namespace Web.DataAccesses.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category entity);
	}
}

