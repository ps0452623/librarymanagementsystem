using DataAcessLayer.Enum;
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

        Task<bool> UpdateReservationStatusAsync(Guid BookReservationId, ReservationStatus Status);
        Task DeleteReservationAsync(Guid Id);


    }
}
