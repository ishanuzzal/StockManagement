using AutoMapper;
using Azure;
using DataAccess.dtos;
using DataAccess.Entities;
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
    public class TransactionService:ITransactionService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IProductRepository productRepository,
            IProductService productService, 
            ITransactionRepository transactionRepository,
            ILogger<TransactionService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<bool>> BuyProduct(AddProductDto addProductDto, string Userid)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var HaveEntity = await _productRepository.GetAsync(e => e.SKU == addProductDto.SKU);
                if (HaveEntity != null)
                {
                    HaveEntity.StockAmount += addProductDto.StockAmount;
                    _productRepository.UpdateAsync(HaveEntity);
                }
                else
                {
                    var product = _mapper.Map<Product>(addProductDto);
                    HaveEntity = await _productRepository.AddAsync(product);
  
                }

                HaveEntity.UserId = Userid;
                var transactionDto = _mapper.Map<BuyTransactionDto>(addProductDto);
                var transaction = _mapper.Map<Transactions>(transactionDto);
                transaction.Products = HaveEntity;
                transaction.TransactionTypes = DataAccess.Enums.TransactionType.Buy;
                var result = await _transactionRepository.AddAsync(transaction);
                
                var changes = await _unitOfWork.Complete();
                if (changes > 0)
                {
                    response.Success = true;
                    response.Data = true;
                }
                else response.Success = false;
            }
            catch(Exception ex ) {
                _logger.LogError(ex.Message);
                throw;
            }

            return response;

        }

        public async Task<ServiceResponse<bool>> SellProduct(SellProductDto sellProductDto, string id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var HaveEntity = await _productRepository.GetAsync(e => e.Id == sellProductDto.Id);
                if(HaveEntity == null)
                {
                    response.Success = false;
                    return response;
                }
                if(sellProductDto.Qty>HaveEntity.StockAmount)
                {
                    response.Success = false;
                    response.Message = "Sell Amount is greater than stock amount";
                    return response;
                }
                
                HaveEntity.StockAmount-= sellProductDto.Qty;
                _productRepository.UpdateAsync(HaveEntity);
                var transactionDto = _mapper.Map<SellTransactionDto>(sellProductDto);
                var transaction = _mapper.Map<Transactions>(transactionDto);
                transaction.UserId = id;

                await _transactionRepository.AddAsync(transaction);
                var changes = await _unitOfWork.Complete();
                if (changes > 0)
                {
                    response.Success = true;
                    response.Data = true; return response;
                }
                response.Success = false;
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message); throw;
            }

            return response;
        }

        public async Task<PaginatedServiceResponse<List<ShowTransactionDto>>> ShowTransactionPaginatedDto(PaginationSortDto paginationSortDto)
        {
            var response = new PaginatedServiceResponse<List<ShowTransactionDto>>();
            try
            {
                var paginatedDto = _mapper.Map<PaginationSortDto_DataAccess>(paginationSortDto);
                var entities = await _transactionRepository.GetPaginatedItemsListAsync(paginatedDto);
                if (entities == null || entities.Data.IsNullOrEmpty())
                {
                    response.Success = false;
                    response.Data = new List<ShowTransactionDto>();
                    response.TotalItemInList = 0;
                    response.TotalItemDataBase = entities?.TotalItemDataBase;
                    return response;
                }
                response.Success = true;
                response.Data = _mapper.Map<List<ShowTransactionDto>>(entities.Data);
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
    }
}
