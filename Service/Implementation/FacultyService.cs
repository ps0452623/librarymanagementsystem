using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAcessLayer.Entities;
using DataAcessLayer;
using DTO;
using Service.Interface;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace Service.Implementation
{
    public class FacultyService : IFacultyService
    {

        private readonly IGenericRepository<Faculty> _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<Designation> _designationRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FacultyService> _logger;

        public FacultyService(IGenericRepository<Faculty> repository, IGenericRepository<Designation> designationRepository, UserManager<ApplicationUser> userManager, IMapper mapper, IWebHostEnvironment webHostEnvironment, ILogger<FacultyService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _userManager = userManager;
            _designationRepository = designationRepository;
        }

        public async Task<IEnumerable<FacultyDto>> GetAll()
        {
            var faculties = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<FacultyDto>>(faculties);
        }

        public async Task<FacultyDto> GetById(Guid id)
        {
            var faculty = await _repository.GetByIdAsync(id);
            return faculty != null ? _mapper.Map<FacultyDto>(faculty) : null;
        }

        public async Task<Faculty> GetByUserIdAsync(Guid userId)
        {
            var faculties = await _repository.GetAllAsync();
            return faculties.FirstOrDefault(f => f.UserId == userId);
        }

        public async Task<String> AddOrUpdate(FacultyDto facultyDto)
        {



            string profilePictureFileName = null;
            if (facultyDto.ProfilePicture != null)
            {
                profilePictureFileName = await UploadFileAsync(facultyDto.ProfilePicture);
            }


            // Check if the faculty already exists in the database
            var existingFaculty = await _repository.GetByIdAsync(facultyDto.Id);

            if (existingFaculty != null)
            {
                var userIdString = existingFaculty.UserId.ToString();
                var userINDb = await _userManager.FindByIdAsync(userIdString);
                userINDb.ProfilePicture = profilePictureFileName;
                await _userManager.UpdateAsync(userINDb);
                // Update existing faculty using AutoMapper
                _mapper.Map(facultyDto, existingFaculty);
                await _repository.UpdateAsync(existingFaculty);


                return "Updated";
            }

            // Assign a new Id only when adding a new faculty
            var newFaculty = _mapper.Map<Faculty>(facultyDto);
            newFaculty.Id = Guid.NewGuid(); // Assign a new unique ID

            await _repository.AddAsync(newFaculty);
            var user = await _userManager.FindByIdAsync(facultyDto.UserId.ToString());
            user.ProfilePicture = profilePictureFileName;
            await _userManager.UpdateAsync(user);

            return "Created";
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

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

        public async Task<FacultyResponseDto> GetFacultyProfileByIdAsync(Guid id)
        {
            var facultyQuery = _repository.GetQueryable(); // Get Faculty IQueryable
            var userQuery = _userManager.Users.AsQueryable(); // Get ApplicationUser IQueryable
            var designationQuery = _designationRepository.GetQueryable(); // Get Designation IQueryable

            var result = await (from faculty in facultyQuery
                                join user in userQuery on faculty.UserId.ToString() equals user.Id.ToString()
                                join designation in designationQuery
                                    on faculty.Designation.Id equals designation.Id into dsg

                                from designation in dsg.DefaultIfEmpty() // Left join to include null values
                                where faculty.Id == id
                                select new FacultyResponseDto
                                {
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Email = user.Email,
                                    PhoneNumber = user.PhoneNumber,
                                    Role = "Faculty",
                                    DesignationName = designation != null ? designation.Name : "N/A",
                                    ProfilePictureUrl = user.ProfilePicture
                                }).FirstOrDefaultAsync();

            return result;
        }



    }
}


  
