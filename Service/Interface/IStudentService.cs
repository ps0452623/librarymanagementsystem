using DTO;


namespace LibraryManagement.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(Guid id);

        
    }
}
