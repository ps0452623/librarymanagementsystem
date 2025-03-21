using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using DTO;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(IGenericRepository<Course> courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id); // Ensure this returns Course, not Task<Course>
            return _mapper.Map<CourseDto>(course);
        }



    }
}

