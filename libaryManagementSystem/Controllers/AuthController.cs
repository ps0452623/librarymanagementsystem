using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using DataAcessLayer;
using DataAccessLayer.Repository;

namespace YourNamespace.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto model)
        {
            if (!ModelState.IsValid)  // ❌ Validation fails, return errors
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(model);
            if (result == null)
                return BadRequest("User registration failed.");

            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await _authService.LoginAsync(model);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { token });
        }
    }
}
