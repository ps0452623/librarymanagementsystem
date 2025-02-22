using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAcessLayer.Entities;
using DataAcessLayer.TempRepository;
using DTO;
using Service.Interface;

namespace Service.Implementation
{
    public class DesignationService : IDesignationService
    {
        private readonly IRepository<Designation> _repository;
        private readonly IMapper _mapper;


        public DesignationService(IRepository<Designation> repository, IMapper mapper)
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

    }
}
