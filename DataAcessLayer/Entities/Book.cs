using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Entities
{
    public class Book
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
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }


  
    }
}
