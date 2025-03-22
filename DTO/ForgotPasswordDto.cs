﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class ForgotPasswordDto
    {
        [Required (ErrorMessage ="Email is Required!")]
        [EmailAddress (ErrorMessage = "Wrong Email Format")] 
        public string Email { get; set; }

    }
}
