using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public  class BranchDto
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "BranchName is required")]
        public string Name { get; set; } = string.Empty;
       
    }
}
