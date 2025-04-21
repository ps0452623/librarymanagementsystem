using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DTO
{
    public class ReservationSearchRequestDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string? BookTitle { get; set; } 
        
        public int NumberOfCopies { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public Guid StatusId { get; set; }  
        public DateTime CreatedOn { get; set; }  

        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 5;

        public string SortBy { get; set; } = "CreatedOn";
        public bool IsAscending { get; set; } = true; 

    }

    public class ReservationSearchResponseDto: ReservationSearchRequestDto
    {
        public Guid Id { get; set; }
        public string Picture { get; set; }
        public Guid BookId { get; set; }
        public string Status { get; set; }

    }
}
