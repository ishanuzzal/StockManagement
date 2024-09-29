using DataAccess.Enums;
using Service.dtos;
using shared.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBusinessEntitiesService
    {
        public Task<ServiceResponse<ShowBusinessEntitiesDto>> GetEntitiesAsync(int id);
        public Task<ServiceResponse<List<ShowBusinessEntitiesDto>>> GetAllEntitiesAsync(BusinessType? userType =null);

        Task<PaginatedServiceResponse<List<ShowBusinessEntitiesDto>>> GetPaginatedServiceEntitesAsync(PaginationSortDto dto);
        public Task<ServiceResponse<ShowBusinessEntitiesDto>> AddEntitiesAsync(AddBusinessEntitiesDto addentitiesDto,string id);
        public Task<ServiceResponse<bool>> RemoveEntitiesAsync(int id);
        public Task<ServiceResponse<ShowBusinessEntitiesDto>> UpdateEntitiesAsync(UpdateBusinessEntitiesDto showUserDto);
    }
}
