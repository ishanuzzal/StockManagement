using Service.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBussinessEntitiesService
    {
        public Task<ServiceResponse<ShowBussinessEntitiesDto>> GetEntitiesAsync(int id);
        public Task<ServiceResponse<List<ShowBussinessEntitiesDto>>> GetAllEntitiesAsync(string UserTypes=null);

        public Task<ServiceResponse<ShowBussinessEntitiesDto>> AddEntitiesAsync(AddBussinessEntitiesDto addentitiesDto,string id);
        public Task<ServiceResponse<bool>> RemoveEntitiesAsync(ShowBussinessEntitiesDto BsEdto);
        public Task<ServiceResponse<ShowBussinessEntitiesDto>> UpdateEntitiesAsync(ShowBussinessEntitiesDto showUserDto);
    }
}
