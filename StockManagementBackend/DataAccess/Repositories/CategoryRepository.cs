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
    public class CategoryRepository: GenericRepository<Categories>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context): base(context) { }

    }
}
