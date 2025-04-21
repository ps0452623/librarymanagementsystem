using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Data;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace Service.Implementation
{
    public class ReservationSearchService : IReservationSearchService
    {
        private readonly AppDbContext _context;

        private readonly IGenericRepository<BookReservation> _reserveRepository;

        private readonly IMapper _mapper;


        public ReservationSearchService(IGenericRepository<BookReservation> reserveRepository, AppDbContext context, IMapper mapper)
        {
            _reserveRepository = reserveRepository;
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<(IEnumerable<ReservationSearchResponseDto> BookReservation, int TotalCount)> GetFilteredReservations(ReservationSearchRequestDto filterRequest)
            {
            var query = _reserveRepository.GetQueryable()
                                  .Include(r => r.User)
                                  .Include(r => r.Book)
                                  .AsQueryable();
            if (!string.IsNullOrEmpty(filterRequest.UserName))
            {
                query = query.Where(r => r.User.UserName.Contains(filterRequest.UserName));
            }

            if (!string.IsNullOrEmpty(filterRequest.BookTitle))
            {
                query = query.Where(r => r.Book.Title.Contains(filterRequest.BookTitle));
            }
            if (filterRequest.StatusId != Guid.Empty)
            {
                query = query.Where(r => r.StatusId == filterRequest.StatusId);
            }
            if (filterRequest.ReservationDate != DateTime.MinValue)
            {
                query = query.Where(r => r.ReservationDate.Date == filterRequest.ReservationDate.Date);
            }
            if (filterRequest.ReturnDate != DateTime.MinValue)
            {
                query = query.Where(r => r.ReturnDate.Date == filterRequest.ReturnDate.Date);
            }
            if (filterRequest.CreatedOn != DateTime.MinValue)
            {
                query = query.Where(r => r.CreatedOn.Date == filterRequest.CreatedOn.Date);
            }
            query = filterRequest.SortBy?.ToLower() switch
            {
                "reservationdate" => filterRequest.IsAscending ? query.OrderBy(r => r.ReservationDate) : query.OrderByDescending(r => r.ReservationDate),
                "returndate" => filterRequest.IsAscending ? query.OrderBy(r => r.ReturnDate) : query.OrderByDescending(r => r.ReturnDate),
                "createdon" => filterRequest.IsAscending ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn),
                _ => query.OrderBy(r => r.ReservationDate)
            };
            int totalCount = await query.CountAsync();
            var reservations = await query.Skip((filterRequest.PageNumber - 1) * filterRequest.PageSize)
                                          .Take(filterRequest.PageSize)
                                          .ToListAsync();

            var reservationDtos = _mapper.Map<IEnumerable<ReservationSearchResponseDto>>(reservations);

            return (reservationDtos, totalCount);
        }

    }
}
