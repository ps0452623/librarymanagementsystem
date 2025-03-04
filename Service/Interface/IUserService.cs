
﻿using DataAcessLayer;
using DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
   public  interface IUserService
    {

        Task<(IEnumerable<ApplicationUser>Users, int TotalCount)> GetUserAsyn(UserFilterRequest request);
        Task<(IEnumerable<RegistrationDto>, int)> GetUserAsync(UserFilterRequest request);

    }
}
