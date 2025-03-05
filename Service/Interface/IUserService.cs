
using DataAcessLayer;
using DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {

        Task<(IEnumerable<UserResponseDto>, int TotalCount)> GetUserAsyn(UserFilterRequest request);

    }
}
