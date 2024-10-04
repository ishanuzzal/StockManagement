using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Serilog;
using Service.dtos;
using Service.Interfaces;
using shared.dtos;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ITransactionService _transactionService;
        private readonly ILogger<ProductController> _logger;    
        
        public ProductController(IProductService productService,ITransactionService transactionService,ILogger<ProductController> logger)
        {
            _productService = productService;   
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost("BuyProduct")]
        [Authorize]
        public async Task<IActionResult> BuyProduct([FromBody] AddProductDto addProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string ValidUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(ValidUserId)) return Unauthorized();
            Log.Error("Product purchase request received: {@AddProductDto}", addProductDto);

            var response = await _transactionService.BuyProduct(addProductDto,ValidUserId);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [HttpPost("SellProduct")]
        [Authorize]
        public async Task<IActionResult> SellProduct([FromBody] SellProductDto sellProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string ValidUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(ValidUserId)) return Unauthorized();
            var response = await _transactionService.SellProduct(sellProductDto, ValidUserId);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("ShowAllProduct")]
        
        public async Task<IActionResult> ShowAllPaginatedProduct([FromQuery] PaginationSortDto paginationSortDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _productService.GetPaginatedProductAsync(paginationSortDto);
            if (!response.Success) return BadRequest();
            return Ok(response);   

        }

        [HttpGet("GetProductQuery")]
        [Authorize]
        public async Task<IActionResult> GetProductQuery([FromQuery] PaginationSortDto dto,[FromQuery] string SKU)
        {
            var response = await _productService.GetProductQuery(dto,SKU);
            if (!response.Success) return BadRequest();
            return Ok(response);   
        }

        
        [HttpGet("GetInventoryReport")]

        public async Task<IActionResult> GetInventoryStatus()
        {
            var response = await _productService.GenerateProductReportAsync();
            if (!response.Success)
            {
                return BadRequest();
            }
            var content = response.Data;
            var fileName = $"InventoryReport_{DateTime.UtcNow:yyyyMMdd}.pdf";
            return File(content, "application/pdf", fileName);
        }
    }
}
