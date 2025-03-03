using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CourseDto
    {
        
        public Guid Id { get; set; }

        [Required(ErrorMessage ="CourseName is required")]
        public string Name { get; set; } = string.Empty;
    }
}
