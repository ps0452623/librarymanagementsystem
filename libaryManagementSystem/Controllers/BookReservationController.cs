using DataAcessLayer.Entities;
using DataAcessLayer.Enum;
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
        private readonly IReservationSearchService _reservationSearch;
        public BookReservationController(IBookReservationService reservation, IReservationSearchService reservationSearch)
        {
            _reservation = reservation;
            _reservationSearch = reservationSearch;
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

        [HttpPut("{Id}/status")]
        public async Task<IActionResult> UpdateReservationStatus(Guid Id, ReservationStatus status)
        {
            var result = await _reservation.UpdateReservationStatusAsync(Id, status);
            if (result)
            {
                return Ok(new { message = "Success", status = "Updated" });
            }
            else
            {
                return BadRequest(new { message = "Failed to update reservation status" });
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BookReservationRequestDto request)
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
                [HttpGet("statuses/GetAll")]
        public IActionResult GetStatuses()
        {
            var statuses = Enum.GetValues(typeof(ReservationStatus))
                                .Cast<ReservationStatus>()
                                .Select(s => new { Id = (int)s, StatusName = s.ToString() })
                                .ToList();

            return new ObjectResult(statuses) { StatusCode = 200 };
        }

        [HttpGet("GetFilteredReservations")]
        public async Task<IActionResult> GetFilteredReservations([FromQuery] ReservationSearchRequestDto filterRequest)
        {
            if (filterRequest.PageNumber <= 0 || filterRequest.PageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than 0.");
            }

            try
            {
                var (reservations, totalCount) = await _reservationSearch.GetFilteredReservations(filterRequest);

                return Ok(new
                {
                    TotalCount = totalCount,
                    Reservations = reservations
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    }

  

