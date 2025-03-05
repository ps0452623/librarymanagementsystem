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
        Task<IEnumerable<BookResponseDto>> GetAll();
        Task<BookResponseDto> GetById(Guid id);
        Task<string> Create(BookRequestDto bookRequestDto);
        Task<string> Update(Guid id, BookRequestDto bookRequestDto);
        Task<string> Delete(Guid id);


    }
}
