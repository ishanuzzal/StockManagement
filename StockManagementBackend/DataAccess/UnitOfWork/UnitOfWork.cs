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

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IProductRepository Products { get; set; }
        public ITransactionRepository transactions { get; }

        public UnitOfWork(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
            Products = new ProductRepository(_appDbContext);
            transactions = new TransactionRepository(_appDbContext);
        }
        public int Complete()
        {
            return _appDbContext.SaveChanges();
        }
        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
