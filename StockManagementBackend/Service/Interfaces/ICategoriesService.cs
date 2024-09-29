using Service.dtos;
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
        public Task<ServiceResponse<List<ShowCategoryDto>>> GetAllCategoriesAsync();

        public Task<ServiceResponse<ShowCategoryDto>> AddCategoryAsync(AddCategoryDto categoryDto,string Id);

        public Task<ServiceResponse<ShowCategoryDto>> UpdateCategoryAsync(AddCategoryDto categoryDto);   

        public Task<ServiceResponse<bool>> DeleteCateogryAsync(int id);

    }
}
