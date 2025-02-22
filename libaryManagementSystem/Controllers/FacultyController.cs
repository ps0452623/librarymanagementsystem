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

        [HttpGet(" GetAll ")]
        public async Task<ActionResult<IEnumerable<FacultyDto>>> GetAll()
        {
            var faculties = await _facultyService.GetAll();
            return Ok(faculties);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<FacultyDto>> GetById(Guid id)
        {
            var faculty = await _facultyService.GetById(id);
            if (faculty == null) return NotFound("Faculty not find");
            return Ok(faculty);
        }
//
//
//      [HttpPost("Add")]
//        public async Task<IActionResult> CreateFaculty([FromBody] FacultyDto facultyDto)
//        {
//            if (facultyDto == null)
//                return BadRequest("Invalid Faculty data.");

//            var success = await _facultyService.Add(facultyDto);
//            if (!success)
//                return StatusCode(500, "Error creating Faculty.");

//            return Ok("Faculty created successfully.");
//        }
//       
    }
}