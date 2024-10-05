using AutoMapper;
using Azure;
using DataAccess.dtos;
using DataAccess.Entities;
using DataAccess.Repositories.IRepositories;
using DataAccess.unitOfWork;
using iTextSharp.text.pdf;
using iTextSharp.text;
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
        private readonly IBussinessEntitiesRepository _bussinessEntitiesRepository;
        private readonly ILogger<TransactionService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IProductRepository productRepository,
            IProductService productService, 
            ITransactionRepository transactionRepository,
            IBussinessEntitiesRepository bussinessEntitiesRepository,
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
            _bussinessEntitiesRepository = bussinessEntitiesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<byte[]>> BuyProduct(AddProductDto addProductDto, string Userid)
        {
            var response = new ServiceResponse<byte[]>();

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
                    var printProduct = _mapper.Map<PrintBuyTransaction>(result);
                    var bsentities = await _bussinessEntitiesRepository.GetAsync(e=>e.Id==result.BussinessEntitiesId);
                    var productentities = await _productRepository.GetAsync(e => e.Id == HaveEntity.Id);
                    printProduct.Buyer = bsentities.Name.ToString();
                    printProduct.ProductName = productentities.Name;
                    using (var ms = new MemoryStream())
                    {
                        Document document = new Document(PageSize.A4, 10, 10, 42, 35);
                        PdfWriter writer = PdfWriter.GetInstance(document, ms);
                        document.Open();

                        var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                        var headerFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                        var bodyFont = FontFactory.GetFont("Arial", 12);
                        var footerFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);

                        document.Add(new Paragraph("Product Purchase Report", titleFont) { Alignment = Element.ALIGN_CENTER });
                        document.Add(new Paragraph($"Generated on: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}", bodyFont) { Alignment = Element.ALIGN_CENTER });
                        document.Add(new Paragraph("\n"));

                        document.Add(new Paragraph($"Seller: {printProduct.Buyer}", headerFont));
                        document.Add(new Paragraph($"Transaction Type: {printProduct.TransactionTypes}", bodyFont));
                        document.Add(new Paragraph("\n\n"));

                        PdfPTable table = new PdfPTable(4); 
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 1.5f, 1f, 1f, 1f });

                        PdfPCell cell = new PdfPCell(new Phrase("Product Purchase Details", headerFont))
                        {
                            Colspan = 4,
                            HorizontalAlignment = 1, 
                            BackgroundColor = new BaseColor(240, 240, 240),
                            Padding = 5
                        };
                        table.AddCell(cell);

                        table.AddCell(new Phrase("Product Name", headerFont));
                        table.AddCell(new Phrase("Quantity", headerFont));
                        table.AddCell(new Phrase("Total Price", headerFont));
                        table.AddCell(new Phrase("Transaction Type", headerFont));

                        table.AddCell(new Phrase(printProduct.ProductName, bodyFont));
                        table.AddCell(new Phrase(transaction.Qty.ToString(), bodyFont));
                        table.AddCell(new Phrase($"{transaction.TotalPrice:0.00}", bodyFont));
                        table.AddCell(new Phrase(transaction.TransactionTypes.ToString(), bodyFont));

                        document.Add(table);

                        document.Add(new Paragraph("\n\n"));
                        document.Add(new Paragraph("Thank you for your business!", footerFont) { Alignment = Element.ALIGN_CENTER });

                        document.Close();

                        response.Success = true;
                        response.Data = ms.ToArray();
                    }

                }
                else response.Success = false;
            }
            catch(Exception ex ) {
                _logger.LogError(ex.Message);
                throw;
            }

            return response;

        }

        public async Task<ServiceResponse<byte[]>> SellProduct(SellProductDto sellProductDto, string id)
        {
            var response = new ServiceResponse<byte[]>();
            try
            {
                var HaveEntity = await _productRepository.GetAsync(e => e.Id == sellProductDto.Id);
                if (HaveEntity == null)
                {
                    response.Success = false;
                    return response;
                }
                if (sellProductDto.Qty > HaveEntity.StockAmount)
                {
                    response.Success = false;
                    response.Message = "Sell Amount is greater than stock amount";
                    return response;
                }

                HaveEntity.StockAmount -= sellProductDto.Qty;
                _productRepository.UpdateAsync(HaveEntity);
                var transactionDto = _mapper.Map<SellTransactionDto>(sellProductDto);
                var transaction = _mapper.Map<Transactions>(transactionDto);
                transaction.UserId = id;

                var result = await _transactionRepository.AddAsync(transaction);
                var changes = await _unitOfWork.Complete();
                if (changes > 0)
                {
                    response.Success = true;
                    var printProduct = _mapper.Map<PrintSellTransaction>(result);
                    var bsentities = await _bussinessEntitiesRepository.GetAsync(e => e.Id == result.BussinessEntitiesId);
                    var productentities = await _productRepository.GetAsync(e => e.Id == HaveEntity.Id);
                    printProduct.Seller = bsentities.Name;
                    printProduct.ProductName = productentities.Name;
                    using (var ms = new MemoryStream())
                    {
                        Document document = new Document(PageSize.A4, 10, 10, 42, 35);
                        PdfWriter writer = PdfWriter.GetInstance(document, ms);
                        document.Open();

                        var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                        var headerFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                        var bodyFont = FontFactory.GetFont("Arial", 12);
                        var footerFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);

                        document.Add(new Paragraph("Product Sell Report", titleFont) { Alignment = Element.ALIGN_CENTER });
                        document.Add(new Paragraph($"Generated on: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}", bodyFont) { Alignment = Element.ALIGN_CENTER });
                        document.Add(new Paragraph("\n"));

                        document.Add(new Paragraph($"Buyer: {printProduct.Seller}", headerFont));
                        document.Add(new Paragraph($"Transaction Type: {printProduct.TransactionTypes}", bodyFont));
                        document.Add(new Paragraph("\n\n"));

                        PdfPTable table = new PdfPTable(4);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 1.5f, 1f, 1f, 1f });

                        PdfPCell cell = new PdfPCell(new Phrase("Product Sell Details", headerFont))
                        {
                            Colspan = 4,
                            HorizontalAlignment = 1,
                            BackgroundColor = new BaseColor(240, 240, 240),
                            Padding = 5
                        };
                        table.AddCell(cell);

                        table.AddCell(new Phrase("Product Name", headerFont));
                        table.AddCell(new Phrase("Quantity", headerFont));
                        table.AddCell(new Phrase("Total Price", headerFont));
                        table.AddCell(new Phrase("Transaction Type", headerFont));

                        table.AddCell(new Phrase(printProduct.ProductName, bodyFont));
                        table.AddCell(new Phrase(transaction.Qty.ToString(), bodyFont));
                        table.AddCell(new Phrase($"{transaction.TotalPrice:0.00}", bodyFont));
                        table.AddCell(new Phrase(transaction.TransactionTypes.ToString(), bodyFont));

                        document.Add(table);

                        document.Add(new Paragraph("\n\n"));
                        document.Add(new Paragraph("Thank you for your business!", footerFont) { Alignment = Element.ALIGN_CENTER });

                        document.Close();

                        response.Success = true;
                        response.Data = ms.ToArray();
                    }


                }
                else response.Success = false;
            }
            catch (Exception ex)
            {
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
