using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace LibaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

       
    }
}
      
   
    

