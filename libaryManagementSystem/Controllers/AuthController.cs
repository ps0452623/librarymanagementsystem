using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using DataAcessLayer;

namespace YourNamespace.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       // private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
         //private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthController(IAuthService authService )//, UserManager<ApplicationUser> userManager , IWebHostEnvironment webHostEnvironment)
        {
            _authService = authService;
           // _userManager = userManager;
           // _webHostEnvironment = webHostEnvironment;

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


       
       
            //[HttpPost("upload-profile-picture/{userId}")]
            //public async Task<IActionResult> UploadProfilePicture(string userId, [FromForm] StudentDto studentDto)
            //{
            //    // Check if file is uploaded
            //    if (studentDto.ProfilePhoto == null || studentDto.ProfilePhoto.Length == 0)
            //        return BadRequest("Invalid file.");

            //    // Find user by ID
            //    var user = await _userManager.FindByIdAsync(userId);
            //    if (user == null)
            //        return NotFound("User not found.");

            //    // Create folder if not exists
            //    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile_pictures");
            //    if (!Directory.Exists(uploadsFolder))
            //        Directory.CreateDirectory(uploadsFolder);

            //    // Generate unique file name
            //    var fileName = $"{userId}{Path.GetExtension(studentDto.ProfilePhoto.FileName)}";
            //    var filePath = Path.Combine(uploadsFolder, fileName);

            //    // Save file
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await studentDto.ProfilePhoto.CopyToAsync(stream);
            //    }

            //    // Update user profile picture path
            //    user.ProfilePicture = $"/profile_pictures/{fileName}";
            //    await _userManager.UpdateAsync(user);

            //    return Ok(new { Message = "Profile picture uploaded successfully!", ProfilePictureUrl = user.ProfilePicture });
            //}
       
    
    }
}

