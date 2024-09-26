using Microsoft.AspNetCore.Mvc;
using Service.dtos;
using Service.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IUserService _userService;
        private readonly Logger<UserController> _logger;
        public UserController(IAuthService auth, Logger<UserController> logger,IUserService userService) {
            _auth = auth;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _auth.Login(loginDto);  
            if(!response.Success) return BadRequest(response.Message);  
            return Ok(response.Data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registraion(UseRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _auth.Registration(registrationDto);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var response = await  _userService.GetUserAsync(id);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);   
        }

        [HttpGet("getAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await _userService.GetAllUsersAsync();
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(ShowUserDto showUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _userService.UpdateUserAsync(showUserDto);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(ShowUserDto showUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _userService.RemoveUserAsync(showUserDto);
            if (!response.Success) return NotFound(response.Message);
            return Ok(response.Data);
        }


    }
}
