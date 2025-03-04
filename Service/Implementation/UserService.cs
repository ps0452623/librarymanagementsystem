using DataAccessLayer.Repository;
using DataAcessLayer;
using DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
   public class UserService :IUserService
    {
        public readonly IGenericRepository<ApplicationUser> _userRepository;

        public UserService(IGenericRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(IEnumerable<RegistrationDto> , int)> GetUserAsync(UserFilterRequest request)
        {
            var query = _userRepository.Query()
         }
        return(query);
    }
}
