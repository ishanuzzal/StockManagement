using DataAccess.Entities;
using Service.dtos;
using shared.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProductService
    {
        public Task<PaginatedServiceResponse<List<ShowProductDto>>> GetProductQuery(PaginationSortDto dto,string sku);
        public Task<ServiceResponse<ShowProductDto>> GetProductAsync(int Id);
        public Task<ServiceResponse<List<ShowProductDto>>> GetAllProductAsync();

        public Task<PaginatedServiceResponse<List<ShowProductDto>>> GetPaginatedProductAsync(PaginationSortDto dto);

        public Task<ServiceResponse<ShowProductDto>> AddProductAsync(AddProductDto productDto, string Id);

        public Task<ServiceResponse<ShowProductDto>> UpdateProductAsync(UpdateProductDto productDto);

        public void UpdateProductAmountAsync(Product product, double amount);

        public Task<ServiceResponse<byte[]>> GenerateProductReportAsync();
        public Task<ServiceResponse<bool>> DeleteProductAsync(int id);
    }
}
