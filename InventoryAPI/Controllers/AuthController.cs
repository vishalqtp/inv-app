
using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            var user = _userService.GetUser(request.Username);
            if (user != null)
            {
                return Conflict("User already exists.");
            }

            _userService.CreateUser(request.Username, request.Password);
            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            var user = _userService.GetUser(request.Username);
            if (user == null || !_userService.VerifyPassword(user, request.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("User's username is invalid.");
            }

            var token = _jwtService.GenerateToken(user.Username);
            return Ok(new { Token = token });
        }
    }
}
