using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibaryManagementSystem.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public RoleController (RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet("GetAllRoles")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => new { r.Id, r.Name }).ToList();
            return Ok(roles);
        }
        }
}
