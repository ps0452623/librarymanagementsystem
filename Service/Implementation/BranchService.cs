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
    public class BranchService : IBranchService
    {
        private readonly IGenericRepository<Branch> _branchRepository;
        private readonly IMapper _mapper;

        public BranchService(IGenericRepository<Branch> branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BranchDto>> GetAllAsync()
        {
            var Branches = await _branchRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BranchDto>>(Branches);
        }

        public async Task<BranchDto> GetByIdAsync(Guid id)
        {
            var branch = await _branchRepository.GetByIdAsync(id); // ✅ Await the Task
            return _mapper.Map<BranchDto>(branch); // ✅ Now mapping Branch -> BranchDto
        }



    }
}

