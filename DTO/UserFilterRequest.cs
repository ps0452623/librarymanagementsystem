using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserFilterRequest
    {
        public string Search { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string SortColumn { get; set; } = "FirstName";
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;


    }

    public class UserResponseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = string.Empty;


    }

}
