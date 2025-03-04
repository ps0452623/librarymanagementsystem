using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer;
using DataAcessLayer.Entities;
using DTO;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Interface;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<StudentService> _logger;
        public StudentService(IGenericRepository<Student> studentRepository, UserManager<ApplicationUser> userManager,
        IMapper mapper, IWebHostEnvironment webHostEnvironment, ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _userManager = userManager;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<Student> GetByUserIdAsync(Guid userId)
        {
            var Students = await _studentRepository.GetAllAsync();
            return Students.FirstOrDefault(f => f.UserId == userId);
        }


        public async Task<StudentResponseDto> GetStudentDetailByIdAsync(Guid Id)
        {
            var student = await _studentRepository
                .GetQueryable()
                .Include(s => s.User) // Join with AspNetUsers
                .Include(s => s.Branch)         // Join with Branch
                    .ThenInclude(b => b.Course) // Join Branch with Course
                .FirstOrDefaultAsync(s => s.UserId == Id);

            if (student == null)
                return null;

            return new StudentResponseDto
            {

                FirstName = student.User.FirstName,
                LastName = student.User.LastName,
                FatherName = student.FatherName,
                MotherName = student.MotherName,
                RollNumber = student.RollNumber,
                Semester = student.Semester,
                EmergencyContactNumber = student.EmergencyContactNumber,
                BranchName = student.Branch.Name,  // Fetch Branch Name
                CourseName = student.Branch.Course.Name,  // Fetch Course Name
                ProfilePicture = student.User.ProfilePicture
            };
        }





        public async Task<ApplicationUser> FindUserByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<String> AddOrUpdateStudentAsync(StudentDto studentDto)
        {
            
            string profilePictureFileName = null;
            if (studentDto.ProfilePicture != null)
            {
                profilePictureFileName = await UploadFileAsync(studentDto.ProfilePicture);
            }
            // Check if the faculty already exists in the database
            var existingStudent = await _studentRepository.GetByIdAsync(studentDto.Id);

            if (existingStudent != null)
            {
                var userIdString = existingStudent.UserId.ToString();
                var userINDb = await _userManager.FindByIdAsync(userIdString);
                userINDb.ProfilePicture = profilePictureFileName;
                await _userManager.UpdateAsync(userINDb);

                // Update existing faculty using AutoMapper
                _mapper.Map(studentDto, existingStudent);
                await _studentRepository.UpdateAsync(existingStudent);

                return "Updated";
            }
            // Assign a new Id only when adding a new faculty
            var newStudent = _mapper.Map<Student>(studentDto);
            newStudent.Id = Guid.NewGuid(); // Assign a new unique ID
            await _studentRepository.AddAsync(newStudent);

           
            var user = await _userManager.FindByIdAsync(studentDto.UserId.ToString());
            
            
                user.ProfilePicture = await UploadFileAsync(studentDto.ProfilePicture);
                await _userManager.UpdateAsync(user);
            


            return "Created";
        }


        
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }


                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }

                string fileUrl = $"/uploads/{uniqueFileName}";
                _logger.LogInformation($"File uploaded successfully: {fileUrl}");

                return fileUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError($"File upload failed: {ex.Message}");
                throw new Exception("File upload failed.", ex);
            }
        }


    }
}




