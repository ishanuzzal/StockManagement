using DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.dtos;
using Service.Interfaces;
using Service.services;
using System.Security.Claims;
using shared.dtos;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessEntitiesController : Controller
    {
        private readonly IBusinessEntitiesService _bussinessEntities;
        private readonly ILogger<BusinessEntitiesController> _logger;
        
        public BusinessEntitiesController(ILogger<BusinessEntitiesController> logger, IBusinessEntitiesService bussinessEntities)
        {
            _bussinessEntities = bussinessEntities;
            _logger = logger;
        }

        [HttpGet("getBusinessEntities/{id}")]
        public async Task<IActionResult> GetBusinessEntities([FromRoute] int id)
        {
            var response = await _bussinessEntities.GetEntitiesAsync(id);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("getAllBusinessEntities")]
        public async Task<IActionResult> GetAllBusinessEntities([FromQuery] BusinessType? UserType)
        {
            if (UserType == null) return BadRequest();
            var response = await _bussinessEntities.GetAllEntitiesAsync(UserType);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("ShowAllBusinessEntities")]
        public async Task<IActionResult> ShowAllBusinessEntities([FromQuery] PaginationSortDto paginationSortDto)
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
            var response = await _bussinessEntities.GetPaginatedServiceEntitesAsync(paginationSortDto);
            if (!response.Success) return NotFound();
            return Ok(response);

        }

        [Authorize]
        [HttpPost("AddBusinessEntities")]
        public async Task<IActionResult> AddBusinessEntities([FromBody]AddBusinessEntitiesDto AddBusinessEntitiesDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            string ValidUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(ValidUserId)) return Unauthorized();

            var response = await _bussinessEntities.AddEntitiesAsync(AddBusinessEntitiesDto, ValidUserId);
            if(!response.Success) return BadRequest(response.Message);  
            return Ok(response.Data);   

        }

        [HttpPut("UpdateBusinessEntities")]
        public async Task<IActionResult> UpdateBusinessEntities([FromBody]UpdateBusinessEntitiesDto BsEdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _bussinessEntities.UpdateEntitiesAsync(BsEdto);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpDelete("DeleteBusinessEntities/{id}")]
        public async Task<IActionResult> DeleteBusinessEntities(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _bussinessEntities.RemoveEntitiesAsync(id);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }
    }
}
