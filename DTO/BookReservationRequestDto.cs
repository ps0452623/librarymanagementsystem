using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BookReservationRequestDto
    {
        [Required(ErrorMessage ="UserID Is Required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "BookId Is Required")]
        public Guid BookId { get; set; }

        
    }
    public class BookReservationResponseDto : BookReservationRequestDto
    {
        public Guid ReservationId { get; set; }
        public string BookTitle { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
