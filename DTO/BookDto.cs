using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class BookRequestDto
    {
        [Required(ErrorMessage = "Book Title is Necessary!")]

        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Author Name!")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Enter Publisher Name!")]
        public string Publisher { get; set; }
        public string Genre { get; set; }
        [Required(ErrorMessage = "ISBN Number is Necessary!")]
        [MinLength(13, ErrorMessage = "ISBN code must be of 13 Digits")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Enetr Year in which Book Published!")]
        public int YearPublished { get; set; }
        public int CopiesAvailable { get; set; }
        [Required(ErrorMessage = "Book Shelf Number is Necessary!")]
        public int BookShelfNumber { get; set; }
        public IFormFile? Picture { get; set; }
        [Required(ErrorMessage ="Branch is Required")]
        public Guid CourseId { get; set; }

        public Guid BranchId { get; set; }


    }
    public class BookResponseDto
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int YearPublished { get; set; }
        public int CopiesAvailable { get; set; }
        public int BookShelfNumber { get; set; }
        public string Picture { get; set; }
        public string BranchName { get; set; }
        public Guid CourseId { get; set; }



    }
}
