using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.dtos;
using Service.Interfaces;
using Service.services;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BussinessEntitiesController : Controller
    {
        private readonly IBussinessEntitiesService _bussinessEntities;
        private readonly ILogger<UserController> _logger;
        
        public BussinessEntitiesController(ILogger<UserController> logger, IBussinessEntitiesService bussinessEntities)
        {
            _bussinessEntities = bussinessEntities;
            _logger = logger;
        }

        [HttpGet("getBussinessEntities/{id}")]
        public async Task<IActionResult> GetBussinessEntities([FromRoute] int id)
        {
            var response = await _bussinessEntities.GetEntitiesAsync(id);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("getAllEntities")]
        public async Task<IActionResult> GetAllBussinessEntities([FromQuery] string UserType)
        {
            var response = await _bussinessEntities.GetAllEntitiesAsync(UserType);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [Authorize]
        [HttpPost("AddBussinessEntities")]
        public async Task<IActionResult> AddBussinessEntities(AddBussinessEntitiesDto addBussinessEntitiesDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            string ValidUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(ValidUserId)) return Unauthorized();

            var response = await _bussinessEntities.AddEntitiesAsync(addBussinessEntitiesDto, ValidUserId);
            if(!response.Success) return BadRequest(response.Message);  
            return Ok(response.Data);   

        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateBussinessEntities(ShowBussinessEntitiesDto BsEdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _bussinessEntities.UpdateEntitiesAsync(BsEdto);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteBussinessEntities(ShowBussinessEntitiesDto BsEdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _bussinessEntities.RemoveEntitiesAsync(BsEdto);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }
    }
}
