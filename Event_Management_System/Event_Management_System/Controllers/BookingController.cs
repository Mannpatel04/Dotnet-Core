using Event_Management_System.Application.DTOs.BookingDto;
using Event_Management_System.Application.Interface;
using Event_Management_System.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingServices _bookingService;
        public BookingController(IBookingServices bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateBookingDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            var result = await _bookingService.RegisterEvent(userId, dto);

            return Ok(result);
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> Cancel(Guid bookingId)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            await _bookingService.CancelBooking(bookingId, userId);

            return Ok("Booking cancelled");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookings();

            return Ok(bookings);
        }

        [HttpGet("GetbookingsbyOrganizerId")]
        [Authorize(Roles = "Admin, Organizer")]

        public async Task<IActionResult> GetRegistrationsByOrganizer(Guid OrganizerId)
        {
            var result = await _bookingService.GetBookingByorganizerId(OrganizerId);
            return Ok(result);
        }







    }
}
