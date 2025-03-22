using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RegistrationDto
    {
        
            [Required(ErrorMessage = "First Name is required")]
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;


            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email format")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password is required")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Confirm Password is required")]
            [Compare("Password", ErrorMessage = "Passwords do not match")]
            public string ConfirmPassword { get; set; } = string.Empty;

            [MinLength(10, ErrorMessage = "Phone must be at least 10 characters long")]
            public string PhoneNumber { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;



        
    }
}