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
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var faculties = await _facultyService.GetAll();
            return Ok(faculties);
        }


        [HttpPost("Add/Update")]
        public async Task<IActionResult> CreateOrUpdate([FromForm] FacultyDto facultyDto)
        {
            if (facultyDto == null)
                return BadRequest("Invalid Faculty data.");

            var result = await _facultyService.AddOrUpdate(facultyDto);

            if (result == "Created")
            {
                return Ok("Faculty created successfully.");
            }
            if (result == "Updated")
            {
                return Ok("Faculty Updated successfully.");
            }
            else
            {
                return StatusCode(500, "Error processing faculty data.");
            }
        }

        [HttpGet("GetFacultyProfileById{id}")]
        public async Task<ActionResult<FacultyDto>> GetProfileById(Guid id)
        {
            var facultyProfile = await _facultyService.GetFacultyProfileByIdAsync(id);


        if (facultyProfile == null)
        {
            return NotFound("Faculty not found.");
        }

        return Ok(facultyProfile);  
        }

        
    }
}