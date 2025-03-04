using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Genere { get; set; }
        public string ISBN { get; set; }
        public int YearPublished { get; set; }
        public int CopiesAvailable { get; set; }
        public int BookShelfNumber { get; set; }
    }
}
