using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Interface;

namespace Service.Implementation
{
    public class DesignationService : IDesignationService
    {
        private readonly IGenericRepository<Designation> _repository;
        private readonly IMapper _mapper;


        public DesignationService(IGenericRepository<Designation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<DesignationDto>> GetAll()
        {
            var designations = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DesignationDto>>(designations);
        }


      

        public async Task<DesignationDto> GetById(Guid id)
        {
            var designation = await _repository.GetByIdAsync(id);
            return designation != null ? _mapper.Map<DesignationDto>(designation) : null;
        }

        public async Task<string> Add(DesignationDto designationDto)
        {
            if (designationDto == null)
            {
                return "Invalid Data";
            }
            // Map DTO to Entity
            var newDesignation = _mapper.Map<Designation>(designationDto);
            newDesignation.Id = Guid.NewGuid(); // Generate unique ID

            // Save to database
            await _repository.AddAsync(newDesignation);

            return "Created";

        }
    }
}
        
