using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer;
using DataAcessLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class FacultyDto
    {
        public Guid Id { get; set; }
        public Guid DesignationId { get; set; }
        public Guid UserId { get; set; }

        public IFormFile ProfilePicture { get; set; }

    }
    public class FacultyResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = string.Empty;
        public string DesignationName { get; set; }
        public string ProfilePictureUrl{ get; set; }




    }

}
