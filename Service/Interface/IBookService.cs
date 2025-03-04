using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<IEnumerable<BookDto>> GetByIdAsync(Guid id);

        Task<string> AddOrUpdate(FacultyDto facultyDto);




        //Task<IEnumerable<FacultyDto>> GetAll();
        //Task<FacultyDto> GetById(Guid id);
        //Task<string> AddOrUpdate(FacultyDto facultyDto);

        ////Task<bool> Update(FacultyDto facultyDto);
        //Task<Faculty> GetByUserIdAsync(Guid userId);
        //Task<string> UploadFileAsync(IFormFile file);


        //Task<FacultyResponseDto> GetFacultyProfileByIdAsync(Guid id);
        //Task<FacultyResponseDto> GetFacultyProfilesAsync(Guid id);
    }
}
