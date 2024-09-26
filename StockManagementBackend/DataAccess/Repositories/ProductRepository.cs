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
    public class ProductRepository: GenericRepository<Products>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext appDbContext):base(appDbContext) {
            _context = appDbContext;
        }  
    }
}
