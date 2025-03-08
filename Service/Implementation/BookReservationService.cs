using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class BookReservationService : IBookReservationService
    {
        private readonly IGenericRepository<BookReservation> _reserveRepository;
        private readonly IMapper _mapper;


        public BookReservationService(IGenericRepository<BookReservation> reserveRepository, IMapper mapper)
        {
            _reserveRepository = reserveRepository;
            _mapper = mapper;
        }


        //______________GetAll________________________

        public async Task<IEnumerable<BookReservationResponseDto>> GetAllReservationsAsync()
        {
            var reserve = await _reserveRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookReservationResponseDto>>(reserve);

        }

        //_________________GetById_______________________
        public async Task<BookReservationResponseDto> GetReservationByIdAsync(Guid Id)
        {

            var bookReserve = await _reserveRepository.GetByIdAsync(Id);
            if (bookReserve != null)
            {
                return _mapper.Map<BookReservationResponseDto>(bookReserve);
            }
            else
            {
                return null;

            }
        }


        public async Task<BookReservationResponseDto> AddReservationAsync(BookReservationRequestDto request)
        {
            // Create a new reservation object
            var reservation = new BookReservation
            {
                BookId = request.BookId,
                UserId = request.UserId,
                ReservationDate = DateTime.UtcNow,
                StatusId = Guid.Parse("48B956B1-56B8-400A-8158-28B58B6CBF82") // Pending Status
            };

            await _reserveRepository.AddAsync(reservation);

            var addedReservation = await _reserveRepository.GetQueryable()
                 .Include(r => r.Book)
                 .Include(r => r.User)
                  .FirstOrDefaultAsync(r => r.Id == reservation.Id);


            var response = new BookReservationResponseDto
            {
                BookId = addedReservation.BookId,
                BookTitle = addedReservation.Book?.Title,
                UserName = addedReservation.User?.FirstName,
                UserId = addedReservation.UserId,
                Status = "Pending",
                ReservationDate = addedReservation.ReservationDate
            };

            return response;
        
        }

        public async Task<string> UpdateReservationStatusAsync(Guid BookReservationId, Guid StatusId)
        {
            var reservation = await _reserveRepository.GetByIdAsync(BookReservationId);
            if (reservation == null)
                return ("Reservation Not Found");

            reservation.StatusId = StatusId;
            await _reserveRepository.UpdateAsync(reservation);

            return ("ReservationStatus Updated");


        }

    }
    
}
