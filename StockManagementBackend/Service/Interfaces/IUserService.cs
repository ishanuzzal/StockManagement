using Service.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<ShowUserDto>> GetUserAsync(string id);
        public Task<ServiceResponse<List<ShowUserDto>>> GetAllUsersAsync(); 
        public Task<ServiceResponse<bool>> RemoveUserAsync(ShowUserDto showUserDto);
        public Task<ServiceResponse<ShowUserDto>> UpdateUserAsync(ShowUserDto showUserDto);
    }
}
