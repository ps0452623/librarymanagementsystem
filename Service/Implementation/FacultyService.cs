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
using DataAcessLayer.TempRepository;

namespace Service.Implementation
{
    public class FacultyService : IFacultyService
    {

        private readonly IRepository<Faculty> _repository;
            private readonly IMapper _mapper;


            public FacultyService(IRepository<Faculty> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
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


        //    public async Task<bool> Add(FacultyDto employerDto)
        //    {
        //        var employer = _mapper.Map<Faculty>(employerDto);
        //        return await _repository.AddAsync(employer);
        //    }


        //    public async Task<bool> Update(Guid id, FacultyDto facultyDto)
        //    {
        //        var faculty = _mapper.Map<Faculty> (facultyDto);
        //        faculty.Id = id;
        //        return await _repository.UpdateAsync(faculty);
        //    }
        //    public async Task<bool> Delete(Guid id)
        //    {
        //    return await _repository.DeleteAsync(id);

        //}

    }
}
