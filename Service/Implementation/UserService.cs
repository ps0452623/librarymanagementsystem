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


    public class UserService : IUserService

    {
        public readonly IGenericRepository<ApplicationUser> _userRepository;

        public UserService(IGenericRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<(IEnumerable<UserResponseDto>, int TotalCount)> GetUserAsyn(UserFilterRequest request)
        {


            var query = _userRepository.GetQuerable();

            // Apply Search Filter
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(u => u.FirstName.Contains(request.Search) ||
                                         u.PhoneNumber.Contains(request.Search) ||
                                         u.Role.Contains(request.Search) ||
                                         u.Email.Contains(request.Search));
            }
            // Sorting
            query = request.SortColumn?.ToLower() switch
            {
                "name" => request.IsDescending ? query.OrderByDescending(u => u.FirstName) : query.OrderBy(u => u.FirstName),
                "email" => request.IsDescending ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
                "phone" => request.IsDescending ? query.OrderByDescending(u => u.PhoneNumber) : query.OrderBy(u => u.PhoneNumber),
                "Role" => request.IsDescending ? query.OrderByDescending(u => u.Role) : query.OrderBy(u => u.Role),
                _ => query.OrderBy(u => u.FirstName) // Default sorting
            };

            int totalCount = await query.CountAsync();
            var users = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserResponseDto
                {

                    Name = u.FirstName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.Role

                })
                .ToListAsync();

            return (users, totalCount);
        }

    }
}
