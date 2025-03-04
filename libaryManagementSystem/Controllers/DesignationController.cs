using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace LibaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var designations = await _designationService.GetAll();
            return Ok(designations);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DesignationDto>> GetById(Guid id)
        {
            var designation = await _designationService.GetById(id);
            if (designation == null) return NotFound("Designation not found");
            return Ok(designation);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]DesignationDto designationDto)
        {
            if(designationDto == null)
            {
                return BadRequest("Invalid Data");
            }
            var result = await _designationService.Add(designationDto); 

            if(result=="Created")
            {
                return StatusCode(201, "Designation added successfully.");
            }
            if (result != "Created")
            {
                return BadRequest("Failed to add designation.");
            }
             return Ok(result);
            
        }
    }
}
