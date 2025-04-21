using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace Service.Interface
{
    public interface IReservationSearchService
    {
        Task<(IEnumerable<ReservationSearchResponseDto> BookReservation, int TotalCount)> GetFilteredReservations(ReservationSearchRequestDto filterRequest);

    }
}
