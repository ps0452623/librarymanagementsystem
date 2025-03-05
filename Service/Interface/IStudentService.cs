using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagement.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(Guid id);
        Task<String> AddOrUpdateStudentAsync(StudentDto studentDto);
        Task<Student> GetByUserIdAsync(Guid userId);
        Task<string> UploadFileAsync(IFormFile file);

        Task<StudentResponseDto> GetStudentDetailByIdAsync(Guid Id);

    }
}
