﻿using DataAcessLayer.Entities;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
   public  interface IAuthService
    {

        Task<string> LoginAsync(LoginDto model);
        Task<string> RegisterAsync(RegistrationDto model);
        
    }
    }


