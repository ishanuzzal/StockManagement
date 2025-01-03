﻿using AutoMapper;
using DataAccess.dtos;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
using DataAccess.unitOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.dtos;
using Service.Interfaces;
using shared.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoriesService> _logger;
        private readonly IMapper _mapper;
        
        public CategoriesService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, ILogger<CategoriesService> logger,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ShowCategoryDto>> AddCategoryAsync(AddCategoryDto categoryDto,string Id)
        {
            var response = new ServiceResponse<ShowCategoryDto>();
            try
            {
                var HaveEntity = await _categoryRepository.GetAsync(e => e.Name == categoryDto.Name);
                if (HaveEntity != null)
                {
                    response.Success = false;
                    response.Message = "This category is already taken";
                    return response;
                }
                var entity = _mapper.Map<Categories>(categoryDto);
                entity.UsersId = Id;
                var addedCategory = await _categoryRepository.AddAsync(entity);
                var change = await _unitOfWork.Complete();
                if (change > 0)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<ShowCategoryDto>(addedCategory);
                    return response;
                }
                response.Success = false;
                response.Message = "Something went wrong";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); throw;
            }
            return response; 
        }

        public async Task<ServiceResponse<bool>> DeleteCateogryAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var category = await _categoryRepository.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    response.Success = false;
                    response.Message = "Category Not found";
                    return response;
                }

                _categoryRepository.DeleteAsync(category);
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

        public async Task<ServiceResponse<List<ShowCategoryDropdown>>> GetAllCategoriesAsync()
        {
            var response = new ServiceResponse<List<ShowCategoryDropdown>>();
            try
            {
                var categories = await _categoryRepository.GetListAsync();

                if (categories == null || categories.IsNullOrEmpty())
                {
                    response.Success = false;
                    response.Message = "No entitites found found.";
                    response.Data = new List<ShowCategoryDropdown>();
                    return response;
                }
                response.Data = _mapper.Map<List<ShowCategoryDropdown>>(categories);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the list of categories.");
                response.Success = false;
                response.Message = "An error occurred while retrieving the categories.";
                throw;
            }

            return response;

        }

        public async Task<ServiceResponse<ShowCategoryDto>> GetCategoryAsync(int Id)
        {
            var response = new ServiceResponse<ShowCategoryDto>();
            try
            {
                var user = await _categoryRepository.GetAsync(u => u.Id == Id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "category Not found";
                    return response;
                }
                response.Success = true;
                response.Data = _mapper.Map<ShowCategoryDto>(user);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); throw;
            }
            return response;
        }

        public async Task<PaginatedServiceResponse<List<ShowCategoryDto>>> GetPaginatedCategoriesAsync(PaginationSortDto dto)
        {
                var response = new PaginatedServiceResponse<List<ShowCategoryDto>>();
                try
                {
                    var paginatedDto = _mapper.Map<PaginationSortDto_DataAccess>(dto);
                    var entities = await _categoryRepository.GetPaginatedItemsListAsync(paginatedDto);
                    if (entities == null || entities.Data.IsNullOrEmpty())
                    {
                        response.Success = false;
                        response.Data = new List<ShowCategoryDto>();
                        response.TotalItemInList = 0;
                        response.TotalItemDataBase = entities?.TotalItemDataBase;
                        return response;
                    }
                    response.Success = true;
                    response.Data = _mapper.Map<List<ShowCategoryDto>>(entities.Data);
                    response.TotalItemInList = entities.Data?.Count ?? 0;
                    response.TotalItemDataBase = entities.TotalItemDataBase;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    response.Success = false;
                    throw;
                }
                return response;
            
        }

        public async Task<ServiceResponse<ShowCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            var response = new ServiceResponse<ShowCategoryDto>();

            try
            {
                var category = await _categoryRepository.GetAsync(p => p.Id == categoryDto.Id);

                if (category == null)
                {
                    response.Success = false;
                    response.Message = "user not found.";
                    return response;
                }

                _mapper.Map(categoryDto, category);

                _categoryRepository.UpdateAsync(category);

                var change = await _unitOfWork.Complete();

                response.Success = change > 0;
                if (response.Success)
                {
                    var updatedCategory = await _categoryRepository.GetAsync(p=>p.Id == categoryDto.Id);
                    response.Data = _mapper.Map<ShowCategoryDto>(updatedCategory);
                    response.Message = "category updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update failed. No changes made.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Success = false;
                response.Message = "An error occurred while updating the category.";
                throw;
            }

            return response;
        }

        
    }
}
