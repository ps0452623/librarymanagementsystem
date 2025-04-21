using AutoMapper;
using DataAccessLayer.Repository;
using DataAcessLayer.Data;
using DataAcessLayer.Entities;
using DataAcessLayer.Enum;
using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        private readonly IGenericRepository<Book> _bookRepository;

        private readonly IMapper _mapper;


        public BookReservationService(IGenericRepository<BookReservation> reserveRepository, AppDbContext context, IMapper mapper, IGenericRepository<Book> bookRepository)
        {
            _reserveRepository = reserveRepository;
            _mapper = mapper;
            _context = context;
            _bookRepository = bookRepository;
        }
       

        public async Task<IEnumerable<BookReservationResponseDto>> GetAllReservationsAsync()
            {
            var reserve = _reserveRepository.GetQueryable()
                .Include(x => x.Book).AsQueryable()
                .Include(x => x.User).AsQueryable();


            return _mapper.Map<IEnumerable<BookReservationResponseDto>>(reserve);

        }

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

        public async Task AddReservationAsync(BookReservationRequestDto request)
        {
            var reservation = _mapper.Map<BookReservation>(request);
            await _reserveRepository.AddAsync(reservation);
        }


        public async Task<bool> UpdateReservationStatusAsync(Guid BookReservationId, ReservationStatus Status)
        
        {
            var reservation = await _reserveRepository.GetByIdAsync(BookReservationId);
            reservation.Status = Status;
            if (Status == ReservationStatus.Accepted)
            {
                var book = await _bookRepository.GetByIdAsync(reservation.BookId);
                if (book == null)
                    return false;

                if (book.CopiesAvailable < reservation.NumberOfCopies)
                    throw new InvalidOperationException("Not enough copies available.");

                book.CopiesAvailable -= reservation.NumberOfCopies;
                await _bookRepository.UpdateAsync(book);
            }
            if (Status == ReservationStatus.Returned)
            {
                var book = await _bookRepository.GetByIdAsync(reservation.BookId);
                book.CopiesAvailable += reservation.NumberOfCopies;
                await _bookRepository.UpdateAsync(book);
            }

            await _reserveRepository.UpdateAsync(reservation);
            return true;


        }
        public async Task UpdateReservationAsync(Guid Id, BookReservationRequestDto request)
        {
            var reservation = await _reserveRepository.GetByIdAsync(Id);

            reservation.BookId = request.BookId;
            reservation.UserId = request.UserId;
            reservation.NumberOfCopies = request.NumberOfCopies;
            reservation.ReservationDate = request.ReservationDate;
            reservation.ReturnDate = request.ReturnDate;

            await _reserveRepository.UpdateAsync(reservation);
            return;
        }

        public async Task DeleteReservationAsync(Guid Id)
        {
            await _reserveRepository.DeleteAsync(Id);
            return;
        }

       


    }
}