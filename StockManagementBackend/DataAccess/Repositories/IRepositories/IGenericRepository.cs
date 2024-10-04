using DataAccess.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T: class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);


        Task<PaginationDataReturnDto<T>> GetPaginatedItemsListAsync(
                                                                    PaginationSortDto_DataAccess paginationSortDto,
                                                                    Expression<Func<T, bool>> filter=null);
        Task<T> AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
