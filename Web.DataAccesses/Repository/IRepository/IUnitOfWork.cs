using System;
namespace Web.DataAccesses.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository CategoryRepository { get; }
		void Save();
	}
}

