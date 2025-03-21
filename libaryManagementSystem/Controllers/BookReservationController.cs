using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace LibaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReservationController : ControllerBase
    {
        private readonly IBookReservationService _reservation;
        public BookReservationController(IBookReservationService reservation)
        {
            _reservation = reservation;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var reservedbooks = await _reservation.GetAllReservationsAsync();
            return Ok(reservedbooks);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var reservation = await _reservation.GetReservationByIdAsync(Id);
            if (reservation == null)
            {
                return NotFound("reservation Not Found");
            }
            else
            {
                return Ok(reservation);
            }
        }

        [HttpPut("{Id}/status/{statusId}")] 
        public async Task<IActionResult> UpdateReservationStatus( Guid Id, Guid statusId)
        {
            var existingReservation = await _reservation.GetReservationByIdAsync(Id);

            if (existingReservation == null)
            {
                return NotFound("Reservation not found");
            }

            var result = await _reservation.UpdateReservationStatusAsync(Id, statusId);

            if (result == "Updated")
            {
                return Ok("Reservation status updated successfully");
            }

            return BadRequest("Failed to update reservation status");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]BookReservationRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid reservation request.");
            }
            var response = await _reservation.AddReservationAsync(request);
           return Ok(response);
        }



    }
  }

