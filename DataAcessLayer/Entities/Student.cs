using DataAccessLayer;
using DataAccessLayer.Data;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entities
{
   public class Student : BaseEntity
    {
        public string FatherName { get; set; } = string.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string RollNumber { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string EmergencyContactNumber { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

    }

   
}
