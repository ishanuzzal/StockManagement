using DataAccess.DataBaseContext;
using DataAccess.Entities;
using DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TransactionRepository: GenericRepository<Transactions>, ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context):base(context) {
            _context = context;
        }

    }
}
