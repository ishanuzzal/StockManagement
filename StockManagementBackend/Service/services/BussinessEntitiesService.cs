using AutoMapper;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using DataAccess.unitOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.services
{
    public class BussinessEntitiesService : IBussinessEntitiesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBussinessEntitiesRepository _bussinessEntitiesRepository;
        private readonly ILogger<BussinessEntitiesService> _logger;
        private readonly IMapper _mapper;

        public BussinessEntitiesService(IUnitOfWork unitOfWork, IBussinessEntitiesRepository bussinessEntitiesRepository, ILogger<BussinessEntitiesService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bussinessEntitiesRepository = bussinessEntitiesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ShowBussinessEntitiesDto>> AddEntitiesAsync(AddBussinessEntitiesDto addentitiesDto,string id)
        {
            var response = new ServiceResponse<ShowBussinessEntitiesDto>();
            try
            {
                var HaveEntity = await _bussinessEntitiesRepository.GetAsync(e => e.Name == addentitiesDto.Name);
                if(HaveEntity!=null) {
                    response.Success = false;
                    response.Message = "This bussiness entity is already taken";
                    return response;
                }
                var entity = _mapper.Map<BussinessEntities>(addentitiesDto);
                entity.UsersId = id;
                await _bussinessEntitiesRepository.AddAsync(entity);
                var change = await _unitOfWork.Complete();
                if (change > 0)
                {
                    response.Success = true;
                    return response;
                }
                response.Success = false;
                response.Message = "Something went wrong";

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message); throw;
            }
            return response;

        }

        public async Task<ServiceResponse<List<ShowBussinessEntitiesDto>>> GetAllEntitiesAsync(string UserType=null)
        {
            var response = new ServiceResponse<List<ShowBussinessEntitiesDto>>();

            try
            {
                var users = await _bussinessEntitiesRepository.GetListAsync(
                    filter: UserType != null ? (u => u.Type == UserType) : null);

                if (users == null || users.IsNullOrEmpty())
                {
                    response.Success = false;
                    response.Message = "No entitites found found.";
                    response.Data = new List<ShowBussinessEntitiesDto>();
                    return response;
                }
                response.Data = _mapper.Map<List<ShowBussinessEntitiesDto>>(users);
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

        public async Task<ServiceResponse<ShowBussinessEntitiesDto>> GetEntitiesAsync(int id)
        {
            var response = new ServiceResponse<ShowBussinessEntitiesDto>();
            try
            {
                var user = await _bussinessEntitiesRepository.GetAsync(u => u.Id == id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "entities Not found";
                    return response;
                }
                response.Success = true;
                response.Data = _mapper.Map<ShowBussinessEntitiesDto>(user);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); throw;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveEntitiesAsync(ShowBussinessEntitiesDto BsEdto)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var user = await _bussinessEntitiesRepository.GetAsync(u => u.Id == BsEdto.Id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User Not found";
                    return response;
                }

                _bussinessEntitiesRepository.DeleteAsync(user);
                var change = await _unitOfWork.Complete();
                if (change > 0)
                {
                    response.Success = true;
                    return response;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); throw;
            }
            return response;
        }

        public async Task<ServiceResponse<ShowBussinessEntitiesDto>> UpdateEntitiesAsync(ShowBussinessEntitiesDto showUserDto)
        {
            var response = new ServiceResponse<ShowBussinessEntitiesDto>();

            try
            {
                var user = await _bussinessEntitiesRepository.GetAsync(p => p.Id == showUserDto.Id);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "user not found.";
                    return response;
                }

                _mapper.Map(showUserDto, user);

                _bussinessEntitiesRepository.UpdateAsync(user);

                var change = await _unitOfWork.Complete();

                response.Success = change > 0;
                if (response.Success)
                {
                    response.Data = _mapper.Map<ShowBussinessEntitiesDto>(user);
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
