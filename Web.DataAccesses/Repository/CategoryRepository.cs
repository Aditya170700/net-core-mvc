using System;
using System.Linq.Expressions;
using Web.DataAccesses.Data;
using Web.DataAccesses.Repository.IRepository;
using Web.Models;

namespace Web.DataAccesses.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
		{
            _appDbContext = appDbContext;
        }

        public void Update(Category entity)
        {
            _appDbContext.Categories.Update(entity);
        }
    }
}

