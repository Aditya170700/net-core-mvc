using System;
using System.Linq.Expressions;
using Web.DataAccesses.Data;
using Web.DataAccesses.Repository.IRepository;
using Web.Models;

namespace Web.DataAccesses.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
		{
            _appDbContext = appDbContext;
        }

        public void Update(Product entity)
        {
            _appDbContext.Products.Update(entity);
        }
    }
}

