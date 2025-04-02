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

        [Required(ErrorMessage = "Number  Of Book Copies Is Required")]
        public int NumberOfCopies { get; set; }

        [Required(ErrorMessage = "ReservationDate Is Required")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "ReturnDate Is Required")]
        public DateTime ReturnDate { get; set; }

        
    }
    public class BookReservationResponseDto : BookReservationRequestDto
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; }
        public string Picture { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
