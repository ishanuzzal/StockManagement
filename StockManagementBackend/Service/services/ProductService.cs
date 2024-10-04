using AutoMapper;
using DataAccess.dtos;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.IRepositories;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using DataAccess.unitOfWork;

namespace Service.services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository,
            ITransactionRepository transactionRepository,
            ILogger<ProductService> logger, IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

       public async Task<ServiceResponse<ShowProductDto>> AddProductAsync(AddProductDto productDto, string Id)
       {
            var response = new ServiceResponse<ShowProductDto>();
            try
            {
                
                var entity = _mapper.Map<Product>(productDto);
                entity.UserId = Id;
                var addedCategory = await _productRepository.AddAsync(entity);
                _unitOfWork.Complete();
                if (addedCategory!=null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<ShowProductDto>(addedCategory);

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

        public Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ShowProductDto>>> GetAllProductAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedServiceResponse<List<ShowProductDto>>> GetPaginatedProductAsync(PaginationSortDto dto)
        {
            var response = new PaginatedServiceResponse<List<ShowProductDto>>();
            try
            {
                var paginatedDto = _mapper.Map<PaginationSortDto_DataAccess>(dto);
                var entities = await _productRepository.GetPaginatedItemsListAsync(paginatedDto);
                if (entities == null || entities.Data.IsNullOrEmpty())
                {
                    response.Success = false;
                    response.Data = new List<ShowProductDto>();
                    response.TotalItemInList = 0;
                    response.TotalItemDataBase = entities?.TotalItemDataBase;
                    return response;
                }
                response.Success = true;
                response.Data = _mapper.Map<List<ShowProductDto>>(entities.Data);
                foreach (var productDto in response.Data)
                {
                    productDto.StockLevel = CalculateStockLevel(productDto.SKU, productDto.StockAmount);
                }
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


        public async Task<ServiceResponse<ShowProductDto>> GetProductAsync(int Id)
        {
            throw new NotImplementedException();
        }

        private string CalculateStockLevel(string productName, double quantity)
        {

            var prefix = productName.Split('-').FirstOrDefault()?.ToLower();

            switch (prefix)
            {
                case "ch":
                    if (quantity >= 100)
                        return "High";
                    else if (quantity >= 20 && quantity < 100)
                        return "Medium";
                    else
                        return "Low";

                case "tab":
                    if (quantity >= 20)
                        return "High";
                    else if (quantity >= 10 && quantity < 20)
                        return "Medium";
                    else
                        return "Low";

                default:
                    if (quantity >= 50)
                        return "Sufficient";
                    else
                        return "Insufficient";
            }
        }


        public async Task<PaginatedServiceResponse<List<ShowProductDto>>> GetProductQuery(PaginationSortDto dto,string sku)
        {
            var response = new PaginatedServiceResponse<List<ShowProductDto>>();
            try
            {
                var paginatedDto = _mapper.Map<PaginationSortDto_DataAccess>(dto);
                var products = await _productRepository.GetPaginatedItemsListAsync(paginatedDto, e => e.SKU.Contains(sku.ToLower()));
                if (products == null || products.Data.IsNullOrEmpty())
                {
                    response.Success = false;
                    response.Data = new List<ShowProductDto>();
                    response.TotalItemInList = 0;
                    response.TotalItemDataBase = products?.TotalItemDataBase;
                    return response;
                }
                response.Success = true;
                response.Data = _mapper.Map<List<ShowProductDto>>(products.Data);
                foreach (var productDto in response.Data)
                {
                    productDto.StockLevel = CalculateStockLevel(productDto.SKU, productDto.StockAmount);
                }
                response.TotalItemInList = products.Data?.Count ?? 0;
                response.TotalItemDataBase = products.TotalItemDataBase;
            }   
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Success = false;
                throw;
            }
            return response;


        }

        

        public void UpdateProductAmountAsync(Product product, double amount)
        {
            product.StockAmount = amount;   
            _productRepository.UpdateAsync(product);
        }


        public Task<ServiceResponse<ShowProductDto>> UpdateProductAsync(UpdateProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<byte[]>> GenerateProductReportAsync()
        {
            var response = new ServiceResponse<byte[]>();
            try
            {
                var products = await _productRepository.GetListAsync();
                if (products == null)
                {
                    response.Success = false; return response;
                }
                var showProduct = _mapper.Map<List<ShowProductDto>>(products);
                foreach (var productDto in showProduct)
                {
                    productDto.StockLevel = CalculateStockLevel(productDto.SKU, productDto.StockAmount);
                }

                using (var ms = new MemoryStream())
                {
                    Document document = new Document(PageSize.A4, 10, 10, 42, 35);
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();

                    var headerFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
                    var bodyFont = FontFactory.GetFont("Arial", 12);

                    document.Add(new Paragraph($"Company Name: Onorokom", headerFont));
                    document.Add(new Paragraph($"Report Date: {DateTime.UtcNow.ToString("yyyy-MM-dd")}", headerFont));
                    document.Add(new Paragraph("\n"));

                    PdfPTable table = new PdfPTable(7);
                    table.WidthPercentage = 100;

                    table.AddCell("Name");
                    table.AddCell("SKU");
                    table.AddCell("Category");
                    table.AddCell("Stock Level");
                    table.AddCell("Description");
                    table.AddCell("Stock Amount");
                    table.AddCell("Unit Price");

                    // Populate Rows
                    foreach (var product in showProduct)
                    {
                        table.AddCell(product.Name);
                        table.AddCell(product.SKU);
                        table.AddCell(product.CategoryName);
                        table.AddCell(product.StockLevel);
                        table.AddCell(product.Description);
                        table.AddCell(product.StockAmount.ToString());
                        table.AddCell(product.UnitPrice.ToString());
                    }

                    // Add Table to Document
                    document.Add(table);
                    document.Close();
                    response.Success = true;
                    response.Data = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); throw;
            }

            return response;
        }
            
    }
}
