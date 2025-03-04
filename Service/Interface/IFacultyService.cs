using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interface
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDto>> GetAll();
        Task<FacultyDto> GetById(Guid id);
        Task<string> AddOrUpdate(FacultyDto facultyDto);

        //Task<bool> Update(FacultyDto facultyDto);
        Task<Faculty> GetByUserIdAsync(Guid userId);
        Task<string> UploadFileAsync(IFormFile file);


        Task<FacultyResponseDto> GetFacultyProfileByIdAsync(Guid id);
        //Task<FacultyResponseDto> GetFacultyProfilesAsync(Guid id);
    }
}
