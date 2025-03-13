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
        Task<string> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

    }
}


