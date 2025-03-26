using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Implementation;
using Service.Interface;

namespace LibaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var Branches = await _branchService.GetAllAsync();
            return Ok(Branches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null) return NotFound();
            return Ok(branch);
        }

        [HttpGet("GetByCourse/{courseId}")]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetBranchesByCourse(Guid courseId)
        {
            // Call the service method
            var branches = await _branchService.GetBranchesByCourse(courseId);

            // If no branches found, return 404 Not Found
            if (branches == null )
            {
                return NotFound(new { Message = "No branches found for the selected course." });
            }

            // Return the list of branches with 200 OK
            return Ok(branches);
        }


        }


       
    }
