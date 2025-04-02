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

        //[HttpPut("{Id}/status/{statusId}")] 
        //public async Task<IActionResult> UpdateReservationStatus( Guid Id, Guid statusId)
        //{
        //    var existingReservation = await _reservation.GetReservationByIdAsync(Id);

        //    if (existingReservation == null)
        //    {
        //        return NotFound("Reservation not found");
        //    }

        //    var result = await _reservation.UpdateReservationStatusAsync(Id, statusId);

        //    if (result == "Updated")
        //    {
        //        return Ok("Reservation status updated successfully");
        //    }

        //    return BadRequest("Failed to update reservation status");
        //}
       

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]BookReservationRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid reservation request.");
            }
            await _reservation.AddReservationAsync(request);
            return Ok(new { message = "Book issue request has been submitted successfully. Please check the status of your request after sometime." });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateReservation(Guid Id, [FromBody] BookReservationRequestDto request)
        {
            await _reservation.UpdateReservationAsync(Id, request);
            return Ok("BookReservation Updated Successfully");
        }

        [HttpDelete("Delete/{Id}")]
        
        public async Task<IActionResult> DeleteReservationAsync(Guid Id)
        {
            try
            {
                await _reservation.DeleteReservationAsync(Id);
                return Ok(new { message = "Reservation deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the Reservation.", details = ex.Message });
            }
        }

    }
  }

