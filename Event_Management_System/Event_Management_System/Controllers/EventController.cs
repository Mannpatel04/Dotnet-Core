using Event_Management_System.Application.DTOs.EventDto;
using Event_Management_System.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Event_Management_System.Controllers
{
    [EnableRateLimiting("fixed")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventServices _eventService;
        private readonly IBookingServices _bookingService;

        public EventController(IEventServices eventService,  IBookingServices bookingService)
        {
            _eventService = eventService;
            _bookingService = bookingService;
        }

        [Authorize(Roles = "Admin,Organizer")]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventCreateDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            var result = await _eventService.CreateEvent(dto, userId);

            return Ok(result);
        }

        [Authorize(Roles = "Admin,Organizer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, EventUpdateDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            var result = await _eventService.UpdateEvent(id, dto, userId);

            return Ok(result);
        }


        [Authorize(Roles = "Admin,Organizer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            await _eventService.DeleteEvent(id, userId);

            return Ok("Event deleted");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEvents();

            return Ok(events);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var ev = await _eventService.GetEventById(id);

            return Ok(ev);
        }

        [Authorize(Roles = "Admin,Organizer")]
        [HttpGet("{eventId}/registrations")]
        public async Task<IActionResult> GetRegistrations(Guid eventId)
        {
            var registrations = await _bookingService.GetRegistrationsByEvent(eventId);

            return Ok(registrations);
        }

        [Authorize(Roles = "Organizer")]
        [HttpGet("my-events")]
        public async Task<IActionResult> GetMyEvents()
        {
            var organizerId = Guid.Parse(User.FindFirst("UserId").Value);

            var events = await _eventService.GetEventsByOrganizer(organizerId);

            return Ok(events);
        }
    }
}
