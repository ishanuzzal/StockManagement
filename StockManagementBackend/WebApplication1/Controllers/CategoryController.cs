using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.dtos;
using Service.Interfaces;
using shared.dtos;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly ILogger<CategoryController> _logger;   
        
        public CategoryController(ICategoriesService categoriesService,ILogger<CategoryController> logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        [HttpGet("getCategories/{id}")]
        public async Task<IActionResult> GetCategories([FromRoute] int id)
        {
            var response = await _categoriesService.GetCategoryAsync(id);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("getAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoriesService.GetAllCategoriesAsync();
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("ShowAllCategories")]
        public async Task<IActionResult> ShowAllCategories([FromQuery] PaginationSortDto paginationSortDto)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError(error.ErrorMessage);
                    }
                }

                return BadRequest();
            }
            var response = await _categoriesService.GetPaginatedCategoriesAsync(paginationSortDto);
            if (!response.Success) return NotFound();
            return Ok(response);

        }

        [Authorize]
        [HttpPost("AddCategories")]
        public async Task<IActionResult> AddCategories([FromBody] AddCategoryDto AddCategoriesDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            string ValidUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(ValidUserId)) return Unauthorized();

            var response = await _categoriesService.AddCategoryAsync(AddCategoriesDto, ValidUserId);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);

        }

        [HttpPut("UpdateCategories")]
        public async Task<IActionResult> UpdateCategories([FromBody] UpdateCategoryDto BsEdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _categoriesService.UpdateCategoryAsync(BsEdto);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpDelete("DeleteCategories/{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _categoriesService.DeleteCateogryAsync(id);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }
    }
}
