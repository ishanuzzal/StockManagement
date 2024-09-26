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
        public Task<ServiceResponse<VaidUserDto>> Login(UserLoginDto userLoginDto);
        public Task<ServiceResponse<string>> Registration(UseRegistrationDto useRegistrationDto);
    }
}
