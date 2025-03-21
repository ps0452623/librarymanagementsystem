using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entities
{
   public class Branch : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
    
}
