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
        private readonly Logger<UserController> _logger;
        public UserController(IAuthService auth, Logger<UserController> logger) {
            _auth = auth;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto loginDto)
        {
            var response = _auth.Login(loginDto);  
            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Registraion(UseRegistrationDto registrationDto)
        {
            var response = _auth.Registration(registrationDto); 
            return Ok(response);
        }
    }
}
