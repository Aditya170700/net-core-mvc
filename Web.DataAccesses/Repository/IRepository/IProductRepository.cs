using System;
using Web.Models;

namespace Web.DataAccesses.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
        void Update(Product entity);
    }
}

