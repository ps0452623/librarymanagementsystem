using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer;
using DataAcessLayer.Entities;
using DTO;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace LibraryManagement.Controllers
{
    [Route("api/Student")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(IStudentService studentService, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _studentService = studentService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudentById(Guid Id)
        {
            var student = await _studentService.GetByIdAsync(Id);
            if (student == null) return NotFound();
            return Ok(student);

        }

        [HttpGet("GetStudentDetailById/{Id}")]
        public async Task<IActionResult> GetStudentDetailByIdAsync(Guid Id)
        {
            var student = await _studentService.GetStudentDetailByIdAsync(Id);

            if (student == null)

                return NotFound("Student not found");

            return Ok(student);
        }


        [HttpPost("Add/Update")]
        public async Task<IActionResult> AddOrUpdateStudentAsync([FromForm]StudentDto studentDto)
        {
            if (studentDto == null)
                return BadRequest("Invalid Student data.");

            var result = await _studentService.AddOrUpdateStudentAsync(studentDto);

            if (result == "Created")
            {
                return Ok("Student created successfully.");
            }
            if (result == "Updated")
            {
                return Ok("Student Updated successfully.");
            }
            else
            {
                return StatusCode(500, "Error processing Student data.");
            }
        }





        [HttpPost("upload-profile-picture")]

        public async Task<IActionResult> UploadFile(string StudentId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded." });
            }

            try
            {
                string fileUrl = await _studentService.UploadFileAsync(file);
                return Ok(new { filePath = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "File upload failed.", error = ex.Message });
            }
        }


    }
    }




