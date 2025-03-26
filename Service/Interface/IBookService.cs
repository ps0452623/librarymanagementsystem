using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
    public interface IBookService
    {
        IQueryable<BookResponseDto> GetBookQuery();
        Task<IEnumerable<BookResponseDto>> GetAll();
        Task<BookResponseDto> GetById(Guid id);
        Task Create(BookRequestDto bookRequestDto);
        Task Update(Guid id, BookRequestDto bookRequestDto);
        Task Delete(Guid id);
        Task<(IEnumerable<BookSearchResponseDto> Books, int TotalCount)> GetFilteredBooks(BookSearchRequestDto filterRequest);



    }
}
