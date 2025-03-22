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

        [HttpPost("Register")]
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await _authService.LoginAsync(model);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { token });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto email)
        {
            var token = await _authService.ForgotPasswordAsync(email.Email);
            return Ok(new { Token = token, Message = "Use this token to reset your password." });

        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            var isSuccess = await _authService.ResetPasswordAsync(resetPassword.Email, resetPassword.Token, resetPassword.NewPassword);
            if (!isSuccess)
            {
                return BadRequest("Failed to reset password.");
            }
            return Ok("Password reset successfully.");
        }

    }
    
}
