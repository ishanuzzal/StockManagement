using DataAccess.DataBaseContext;
using DataAccess.dtos;
using DataAccess.Entities;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext appDbContext):base(appDbContext) {
            _context = appDbContext;
        }

        public override async Task<PaginationDataReturnDto<Product>> GetPaginatedItemsListAsync(
        PaginationSortDto_DataAccess paginationSortDto,
            Expression<Func<Product, bool>> filter = null)
        {
            IQueryable<Product> query = _context.Set<Product>().Include(p => p.Categories);

            var totalItemsInDatabase = await query.CountAsync();

            if (filter != null)
            {
                query = query.Where(filter);
                totalItemsInDatabase = query.Count();
            }

            if (!string.IsNullOrEmpty(paginationSortDto.SortBy))
            {
                var propertyInfo = typeof(Product).GetProperty(paginationSortDto.SortBy);
                if (propertyInfo != null)
                {
                    string sorting = $"{paginationSortDto.SortBy} {paginationSortDto.SortOrder}";
                    query = query.OrderBy(sorting);
                }
            }

            var skip = (paginationSortDto.PageNumber - 1) * paginationSortDto.PageSize;

            var pagedData = await query.Skip(skip)
                                       .Take(paginationSortDto.PageSize)
                                       .ToListAsync();

            var result = new PaginationDataReturnDto<Product>
            {
                Data = pagedData,                    
                TotalItemDataBase = totalItemsInDatabase,  
                TotalItemInList = pagedData.Count        
            };

            return result;
        }

        public override async Task<List<Product>> GetListAsync(Expression<Func<Product, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            return await (filter == null ? _context.Set<Product>().Include(e=>e.Categories).ToListAsync(cancellationToken) : _context.Set<Product>().Where(filter).Include(e=>e.Categories).ToListAsync(cancellationToken));
        }

    }
}
