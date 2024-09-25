using Service.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        public Task<ApiResponse<VaidUserDto>> Login(UserLoginDto userLoginDto);
        public Task<ApiResponse<string>> Registeration(UseRegistrationDto useRegistrationDto);
    }
}
