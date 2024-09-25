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
    public class UserRepository : GenericRepository<Users> ,IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { } 
    }
}
