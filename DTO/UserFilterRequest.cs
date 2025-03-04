using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public  class UserFilterRequest
    {
        public string Search { get; set; } = string.Empty;
        public string SortColumn { get; set; } = "FirstName";
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
    }

}
