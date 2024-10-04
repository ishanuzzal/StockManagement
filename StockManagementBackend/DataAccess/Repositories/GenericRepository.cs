using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.dtos;
using System.Globalization;
using System.Linq.Dynamic.Core;

namespace DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        public GenericRepository(DbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result =  await _dbContext.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        public  void DeleteAsync(TEntity entity)
        {
             _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await (filter == null ? _dbContext.Set<TEntity>().ToListAsync(cancellationToken) : _dbContext.Set<TEntity>().Where(filter).ToListAsync(cancellationToken));
        }

        public async virtual Task<PaginationDataReturnDto<TEntity>> GetPaginatedItemsListAsync(PaginationSortDto_DataAccess paginationSortDto,
                                                           Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            var totalItemsInDatabase = await query.CountAsync();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(paginationSortDto.SortBy))
            {
                var propertyInfo = typeof(TEntity).GetProperty(paginationSortDto.SortBy);
                if (propertyInfo != null)
                {
                    string sorting = $"{paginationSortDto.SortBy} {paginationSortDto.SortOrder}";
                    query = query.OrderBy(sorting);
                        
                }
            }

            var skip = (paginationSortDto.PageNumber - 1) * paginationSortDto.PageSize;

            query = query.Skip(skip).Take(paginationSortDto.PageSize);
            var pagedData = await query.Skip(skip).Take(paginationSortDto.PageSize).ToListAsync();

            var result = new PaginationDataReturnDto<TEntity>
            {
                Data = pagedData,  
                TotalItemDataBase = totalItemsInDatabase,  
                TotalItemInList = pagedData.Count         
            };

            return result;
        }


        public async Task<List<TEntity>> GetListQueryAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await (filter == null ? _dbContext.Set<TEntity>().ToListAsync(cancellationToken) : _dbContext.Set<TEntity>().Where(filter).ToListAsync(cancellationToken));
        }

        public void UpdateAsync(TEntity entity)
        {
             _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
