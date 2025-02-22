using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using DTO;
using LibraryManagement.Services.Interfaces;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IGenericRepository<Student>  studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetByIdAsync(Guid id)
        {
            var student = _studentRepository.GetByIdAsync( id);
            return student == null ? null : _mapper.Map<StudentDto>(student);
        }

        
    }
}
