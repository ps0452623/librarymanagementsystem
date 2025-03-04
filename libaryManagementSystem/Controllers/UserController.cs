using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace LibaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilterRequest request)
        {
            var (users, totalCount) = await _userService.GetUserAsyn(request);

            return Ok(new
            {
                TotalCount = totalCount,
                Users = users
            });
        }
    }
}
