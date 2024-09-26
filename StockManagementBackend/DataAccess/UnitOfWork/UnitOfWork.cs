using DataAccess.DataBaseContext;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.unitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IProductRepository Products { get; set; }
        public ITransactionRepository transactions { get; }

        public IUserRepository Users { get; set; }

        public UnitOfWork(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
            Products = new ProductRepository(_appDbContext);
            transactions = new TransactionRepository(_appDbContext);
            Users = new UserRepository(_appDbContext);
        }
        public Task<int> Complete()
        {
            return _appDbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
