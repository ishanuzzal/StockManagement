using Service.dtos;
using shared.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITransactionService
    {
        public Task<ServiceResponse<bool>> BuyProduct(AddProductDto addProductDto, string id);
        public Task<ServiceResponse<bool>> SellProduct(SellProductDto sellProductDto, string id);

        public Task<PaginatedServiceResponse<List<ShowTransactionDto>>> ShowTransactionPaginatedDto(PaginationSortDto paginationSortDto); 
    }
}
