using System;
using Web.DataAccesses.Data;
using Web.DataAccesses.Repository.IRepository;

namespace Web.DataAccesses.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly AppDbContext _appDbContext;
        public ICategoryRepository CategoryRepository { get; private set; }

		public UnitOfWork(AppDbContext appDbContext)
		{
            _appDbContext = appDbContext;
            CategoryRepository = new CategoryRepository(_appDbContext);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}

