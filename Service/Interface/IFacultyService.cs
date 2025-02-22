using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entities;
using DTO;

namespace Service.Interface
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDto>> GetAll();
        Task<FacultyDto> GetById(Guid id);
        //Task<bool> Add(FacultyDto facultyDto);
    
    }
}
