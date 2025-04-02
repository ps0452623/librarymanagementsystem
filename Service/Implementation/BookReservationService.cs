using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Data;
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
        private readonly AppDbContext _context;

        private readonly IGenericRepository<BookReservation> _reserveRepository;

        private readonly IMapper _mapper;


        public BookReservationService(IGenericRepository<BookReservation> reserveRepository, AppDbContext context, IMapper mapper)
        {
            _reserveRepository = reserveRepository;
            _mapper = mapper;
            _context = context;
        }


        //______________GetAll________________________

        public async Task<IEnumerable<BookReservationResponseDto>> GetAllReservationsAsync()
        {
            var reserve = _reserveRepository.GetQueryable()
                .Include(x => x.Book).AsQueryable()
                .Include(x => x.User).AsQueryable();


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

        //_________________Post________________________________________
        public async Task AddReservationAsync(BookReservationRequestDto request)
        {
            var reservation = _mapper.Map<BookReservation>(request);
            await _reserveRepository.AddAsync(reservation);
        }
        //public async Task<string> UpdateReservationStatusAsync(Guid BookReservationId, Guid StatusId)
        //{
        //    var reservation = await _reserveRepository.GetByIdAsync(BookReservationId);
        //    if (reservation == null)
        //        return ("Reservation Not Found");

        //    reservation.StatusId = StatusId;
        //    await _reserveRepository.UpdateAsync(reservation);

        //    return ("ReservationStatus Updated");


        //}
        //-------------------Put-----------------//
        public async Task UpdateReservationAsync(Guid Id, BookReservationRequestDto request)
        {
            var reservation = await _reserveRepository.GetByIdAsync(Id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

           
            reservation.BookId = request.BookId;
            reservation.UserId = request.UserId;
            reservation.NumberOfCopies = request.NumberOfCopies;
            reservation.ReservationDate = request.ReservationDate;
            reservation.ReturnDate = request.ReturnDate;

            await _reserveRepository.UpdateAsync(reservation);
            return ;
        }





        //----------------------Delete---------------------//
        public async Task DeleteReservationAsync(Guid Id)
        {
            await _reserveRepository.DeleteAsync(Id);
            return;
        }
    }

}
