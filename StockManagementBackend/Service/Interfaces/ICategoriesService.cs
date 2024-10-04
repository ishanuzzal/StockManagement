using Service.dtos;
using shared.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoriesService
    {
        public Task<ServiceResponse<ShowCategoryDto>> GetCategoryAsync(int Id);
        public Task<ServiceResponse<List<ShowCategoryDropdown>>> GetAllCategoriesAsync();

        public Task<PaginatedServiceResponse<List<ShowCategoryDto>>> GetPaginatedCategoriesAsync(PaginationSortDto dto);

        public Task<ServiceResponse<ShowCategoryDto>> AddCategoryAsync(AddCategoryDto categoryDto,string Id);

        public Task<ServiceResponse<ShowCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto categoryDto);   

        public Task<ServiceResponse<bool>> DeleteCateogryAsync(int id);

    }
}
