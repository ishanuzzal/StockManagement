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
    public class BussinessEntitiesRepository:GenericRepository<BussinessEntities>, IBussinessEntitiesRepository
    {
        public BussinessEntitiesRepository(AppDbContext context):base(context) { }
    }
}
