using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entities;
using DTO;
using static System.Reflection.Metadata.BlobBuilder;

namespace Service.Interface
{
    public interface IBookFilterService
    {
        Task<(IEnumerable<BookSearchResponseDto> Books, int TotalCount)> GetFilteredBooks(BookSearchRequestDto filterRequest);

    }
}
