using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StudentDto
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "UserID is required")]

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Father Name is required")]
        public string FatherName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Father Name is required")]
        public string MotherName { get; set; } = string.Empty;



        [Required(ErrorMessage = "Rollnumber is Required")]
        public string RollNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Semester is Required")]
        public string Semester { get; set; } = string.Empty;


        public Guid BranchId { get; set; }


        [Required(ErrorMessage = "EmergencyContactNumber is Required")]
        [MinLength(10, ErrorMessage = "EmergencyContactNumber must be aleast 10 characters Long ")]
        public string EmergencyContactNumber { get; set; } = String.Empty;

        public IFormFile ProfilePicture { get; set; }
    }


    public class StudentResponseDto
    {

        public string FirstName { get; set; } = string.Empty; // From AspNetUsers
        public string LastName { get; set; } = string.Empty;  // From AspNetUsers
        public string FatherName { get; set; } = string.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string RollNumber { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string EmergencyContactNumber { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;  // From Branch Table
        public string CourseName { get; set; } = string.Empty;      // From Course Table
        public string ProfilePicture { get; set; } = string.Empty;// From AspNetUsers



    }
}

