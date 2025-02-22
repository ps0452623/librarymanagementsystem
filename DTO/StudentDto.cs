using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public  class StudentDto
    {
        [Required(ErrorMessage = "Father Name is required")]
        public string FatherName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Father Name is required")]
        public string MotherName { get; set; } = string.Empty;



        [Required(ErrorMessage = "Rollnumber is Required")]
        public string RollNumber { get; set; } = string.Empty;




        [Required(ErrorMessage = "Semester is Required")]
        public string Semester { get; set; } = string.Empty;


        [Required(ErrorMessage = "EmergencyContactNumber is Required")]
        [MinLength(10, ErrorMessage = "EmergencyContactNumber must be aleast 10 characters Long ")]
        public string EmergencyContactNumber { get; set; } = String.Empty;
    }
}
