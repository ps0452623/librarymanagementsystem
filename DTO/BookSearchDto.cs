using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BookSearchRequestDto
    {
        public string? Title {  get; set; } //Search by title
        public string? Genre { get; set; } //Filter by genre
        public string? YearPublished { get; set; } //Filter by YearPublished
       public string? BranchName { get; set; } //Filter by Branch
        public Guid BranchId { get; set; }
        public string SortBy { get; set; } = "Title"; // Sorting Field
        public bool IsAscending { get; set; } = true; // Ascending/Descending Order
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
    public class BookSearchResponseDto
    {
        public Guid Id { get; set; }
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
        public Guid BranchId { get; set; }

    }
}
