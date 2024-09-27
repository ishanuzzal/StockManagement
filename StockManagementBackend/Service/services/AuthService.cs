using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using DataAccess.unitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Service.dtos;
using Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ILogger<AuthService> _logger;
        public AuthService(
            IUnitOfWork unitOfWork,
            IConfiguration config,
            UserManager<Users> userManager,
            IUserRepository userRepository,
            SignInManager<Users> signInManager,
            ILogger<AuthService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _userManager = userManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _logger = logger;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Signingkey"]));
        }

        public async Task<ServiceResponse<string>> Registration(UseRegistrationDto useRegistrationDto)
        {
            var response = new ServiceResponse<string>();
            var appuser = new Users
            {
                UserName = useRegistrationDto.UserName,
                Email = useRegistrationDto.Email,
            };
            var createUser = await _userManager.CreateAsync(appuser, useRegistrationDto.Password);
            var addRole = await _userManager.AddToRoleAsync(appuser, useRegistrationDto.Role);

            if (createUser.Succeeded && addRole.Succeeded)
            {
                try
                {
                    await _unitOfWork.Complete();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
                response.Success = true; response.Message = "User created";
                return response;
            }
            response.Success = false;
            return response;
        }

        public async Task<ServiceResponse<VaidUserDto>> Login(UserLoginDto userLoginDto)
        {
            var responseDto = new VaidUserDto();
            var response = new ServiceResponse<VaidUserDto>();
            try
            {
                var user = await _userRepository.GetAsync(
                    u => u.Email == userLoginDto.Email);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User Not found";
                    return response;
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
                if (!result.Succeeded) {
                    response.Success = false;
                    response.Message = "Incorrect UserName or Password";
                    return response;
                }
                var role = await _userManager.GetRolesAsync(user);
                responseDto.UserName = user.UserName;
                responseDto.token = GenerateAccessToken(user.Id,user.Email, user.UserName, role[0]);
                response.Success = true;
                response.Data = responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }


            return response;
        }

        private string GenerateAccessToken(string Id,string Email, string UserName, string Role)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Id),
                 new Claim(ClaimTypes.Email, Email),
                 new Claim(ClaimTypes.Name,UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(ClaimTypes.Role,Role)

            };

            var creeds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = creeds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);

        }
        private ClaimsPrincipal GetTokenPrinciple(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Signingkey"]));
            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }
    }
}
