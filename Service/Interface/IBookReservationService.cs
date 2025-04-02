using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookReservationService
    {
        Task<IEnumerable<BookReservationResponseDto>> GetAllReservationsAsync();
        Task<BookReservationResponseDto> GetReservationByIdAsync(Guid id);
        Task AddReservationAsync(BookReservationRequestDto request);
        Task UpdateReservationAsync( Guid Id, BookReservationRequestDto bookrequestDto);

        //Task<string> UpdateReservationStatusAsync(Guid BookReservationId, Guid StatusId);
        Task DeleteReservationAsync(Guid Id);
    }
}
