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

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await (filter == null ? _dbContext.Set<TEntity>().ToListAsync(cancellationToken) : _dbContext.Set<TEntity>().Where(filter).ToListAsync(cancellationToken));
        }

        public async Task<PaginationDataReturnDto<TEntity>> GetPaginatedItemsListAsync(PaginationSortDto_DataAccess paginationSortDto,
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

        public async Task<List<TEntity>> GetListQueryAsync<TDto>(
        TDto filterDto = null,
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default) where TDto : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            // Apply predefined filter (if provided)
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply dynamic filter based on DTO properties
            if (filterDto != null)
            {
                query = ApplyDynamicFilter(query, filterDto);
            }

            // Apply sorting (if orderBy function is provided)
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Apply pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Execute the query and return the list of entities
            return await query.ToListAsync(cancellationToken);
        }

        private IQueryable<TEntity> ApplyDynamicFilter<TDto>(IQueryable<TEntity> query, TDto filterDto) where TDto : class
        {
            // Get all properties of the DTO object
            var properties = typeof(TDto).GetProperties();

            // Iterate through each property and dynamically apply filters if the value is not null or empty
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(filterDto);

                // Ignore null or empty string values
                if (propertyValue == null || (property.PropertyType == typeof(string) && string.IsNullOrWhiteSpace((string)propertyValue)))
                {
                    continue;
                }

                // Build the lambda expression dynamically
                var parameter = Expression.Parameter(typeof(TEntity), "e");  // Represents the entity (e)
                var propertyExpression = Expression.Property(parameter, property.Name);  // e.PropertyName

                // Ensure the entity has the property with the same name and type
                var entityProperty = typeof(TEntity).GetProperty(property.Name);
                if (entityProperty == null || entityProperty.PropertyType != property.PropertyType)
                {
                    continue;  // Skip if property not found or type mismatch
                }

                // Build the condition e.PropertyName == propertyValue
                var condition = Expression.Equal(propertyExpression, Expression.Constant(propertyValue, entityProperty.PropertyType));

                // Combine with the existing expression using Expression.AndAlso
                var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);

                // Apply the filter to the query
                query = query.Where(lambda);
            }

            return query;
        }


        public void UpdateAsync(TEntity entity)
        {
             _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
