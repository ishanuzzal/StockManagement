using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Repositories.IRepositories
{
    public interface IBussinessEntitiesRepository: IGenericRepository<BussinessEntities>
    {
    }
}
