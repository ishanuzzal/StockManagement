using AutoMapper;
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using DataAccess.unitOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.dtos;
using shared;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.dtos;
using DataAccess.dtos;

namespace Service.services
{
    public class BusinessEntitiesService : IBusinessEntitiesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBussinessEntitiesRepository _bussinessEntitiesRepository;
        private readonly ILogger<BusinessEntitiesService> _logger;
        private readonly IMapper _mapper;

        public BusinessEntitiesService(IUnitOfWork unitOfWork, IBussinessEntitiesRepository bussinessEntitiesRepository, ILogger<BusinessEntitiesService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bussinessEntitiesRepository = bussinessEntitiesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ShowBusinessEntitiesDto>> AddEntitiesAsync(AddBusinessEntitiesDto addentitiesDto,string id)
        {
            var response = new ServiceResponse<ShowBusinessEntitiesDto>();
            try
            {
                var HaveEntity = await _bussinessEntitiesRepository.GetAsync(e => e.Name == addentitiesDto.Name);
                if(HaveEntity!=null && HaveEntity.Type.ToString() == addentitiesDto.Type) {
                    response.Success = false;
                    response.Message = "This Business entity is already taken";
                    return response;
                }
                var entity = _mapper.Map<BussinessEntities>(addentitiesDto);
                entity.UsersId = id;
                var addedEntity = await _bussinessEntitiesRepository.AddAsync(entity);
                var change = await _unitOfWork.Complete();
                if (change > 0)
                {
                    response.Success = true;
                    response.Message = "entities added";
                    response.Data = _mapper.Map<ShowBusinessEntitiesDto>(addedEntity);
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

        public async Task<PaginatedServiceResponse<List<ShowBusinessEntitiesDto>>> GetPaginatedServiceEntitesAsync(PaginationSortDto dto)
        {
            var response = new PaginatedServiceResponse<List<ShowBusinessEntitiesDto>>();
            try
            {
                var paginatedDto = _mapper.Map<PaginationSortDto_DataAccess>(dto);
                var entities = await _bussinessEntitiesRepository.GetPaginatedItemsListAsync(paginatedDto);
                if(entities==null || entities.Data.IsNullOrEmpty())
                {
                    response.Success = false;
                    response.Data = new List<ShowBusinessEntitiesDto>();
                    response.TotalItemInList = 0;
                    response.TotalItemDataBase = entities?.TotalItemDataBase;
                    return response;
                }
                response.Success = true;
                response.Data = _mapper.Map<List<ShowBusinessEntitiesDto>>(entities.Data);
                response.TotalItemInList = entities.Data?.Count ?? 0;
                response.TotalItemDataBase = entities.TotalItemDataBase;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Success = false;
                throw;
            }
            return response;
        }
        public async Task<ServiceResponse<List<ShowBusinessEntitiesDto>>> GetAllEntitiesAsync(BusinessType? UserType = null)
        {
            var response = new ServiceResponse<List<ShowBusinessEntitiesDto>>();

            try
            {
                var users = await _bussinessEntitiesRepository.GetListAsync(
                    filter: UserType != null ? (u => u.Type == UserType) : null);

                if (users == null || users.IsNullOrEmpty())
                {
                    response.Success = false;
                    response.Message = "No entitites found found.";
                    response.Data = new List<ShowBusinessEntitiesDto>();
                    return response;
                }
                response.Data = _mapper.Map<List<ShowBusinessEntitiesDto>>(users);
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

        public async Task<ServiceResponse<ShowBusinessEntitiesDto>> GetEntitiesAsync(int id)
        {
            var response = new ServiceResponse<ShowBusinessEntitiesDto>();
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
                response.Data = _mapper.Map<ShowBusinessEntitiesDto>(user);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); throw;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveEntitiesAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var user = await _bussinessEntitiesRepository.GetAsync(u => u.Id == id);
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

        public async Task<ServiceResponse<ShowBusinessEntitiesDto>> UpdateEntitiesAsync(UpdateBusinessEntitiesDto showUserDto)
        {
            var response = new ServiceResponse<ShowBusinessEntitiesDto>();

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
                    var updatedUser = await _bussinessEntitiesRepository.GetAsync(u=>u.Id == showUserDto.Id);   

                    response.Data = _mapper.Map<ShowBusinessEntitiesDto>(updatedUser);
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
