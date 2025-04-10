﻿using DataAcessLayer.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDto>> GetAllAsync();
        Task<BranchDto> GetByIdAsync(Guid Id);
        Task<IEnumerable<BranchDto>> GetBranchesByCourse(Guid courseId);


    }
}

