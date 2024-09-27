using AutoMapper;
using Azure;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using DataAccess.unitOfWork;
using Microsoft.Extensions.Logging;
using Service.dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ShowUserDto>> GetUserAsync(string id)
        {
            var response = new ServiceResponse<ShowUserDto>();
            try
            {
                var user = await _userRepository.GetAsync(u=>u.Id == id); 
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User Not found";
                    return response;
                }
                response.Success = true;
                response.Data = _mapper.Map<ShowUserDto>(user);

            }
            catch(Exception ex) {
                _logger.LogError(ex.Message); throw;
            }
            return response;

        }

        public async Task<ServiceResponse<List<ShowUserDto>>> GetAllUsersAsync()
        {
            var response = new ServiceResponse<List<ShowUserDto>>();

            try
            {
                var users = await _userRepository.GetListAsync();
                if (users == null)
                {
                    response.Success = false;
                    response.Message = "No users found.";
                    response.Data = new List<ShowUserDto>();    
                    return response;
                }
                response.Data = _mapper.Map<List<ShowUserDto>>(users);
                response.Success = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the list of users.");
                response.Success = false;
                response.Message = "An error occurred while retrieving the users.";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveUserAsync(ShowUserDto showUserDto)
        {
            var response = new ServiceResponse<bool>(); 
            try
            {
                var user = await _userRepository.GetAsync(u => u.Id == showUserDto.Id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User Not found";
                    return response;
                }

                _userRepository.DeleteAsync(user);
                var change = await _unitOfWork.Complete();
                if (change > 0)
                {
                    response.Success = true;
                    return response;
                }

            }
            catch(Exception ex) { 
                _logger.LogError(ex.Message); throw;   
            }
            return response;
        }

        public async Task<ServiceResponse<ShowUserDto>> UpdateUserAsync(ShowUserDto showUserDto)
        {
            var response = new ServiceResponse<ShowUserDto>();

            try
            {
                var user = await _userRepository.GetAsync(p => p.Id == showUserDto.Id);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "user not found.";
                    return response;
                }

                _mapper.Map(showUserDto, user);

                _userRepository.UpdateAsync(user);

                var change = await _unitOfWork.Complete();

                response.Success = change > 0;
                if (response.Success)
                {
                    response.Data = _mapper.Map<ShowUserDto>(user);
                    response.Message = "user updated successfully.";
                }
                else
                {
                    response.Message = "Update failed. No changes made.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Success = false;
                response.Message = "An error occurred while updating the product.";
                throw;
            }

            return response;
        }

    }
}
