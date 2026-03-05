using Event_Management_System.Application.DTOs.BookingDto;
using Event_Management_System.Application.Interface;
using Event_Management_System.Domain.Model;
using Event_Management_System.Domain.Repository_Interface;

namespace Event_Management_System.Application.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepository _bkr;
        private readonly IEventRepository _evr;
        public BookingServices(IBookingRepository bkr, IEventRepository evr)
        {
            _bkr = bkr;
            _evr = evr;
        }


        public async Task<BookingResponseDto> RegisterEvent(Guid userId, CreateBookingDto dto)
        {
            var ev = await _evr.GetById(dto.EventId);

            if (ev == null)
                throw new Exception("Event not found");


            var alreadyBooked = await _bkr.IsExistEvent(dto.EventId, userId);

            if (alreadyBooked)
                throw new Exception("User already registered for this event");

            
            var bookingCount = await _bkr.CountByEventId(dto.EventId);

            if (bookingCount >= ev.capacity)
                throw new Exception("Event capacity reached");


            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                EventId = dto.EventId,
                UserId = userId,
                BookedAt = DateTime.UtcNow
            };

            await _bkr.Addbooking(booking);

            return new BookingResponseDto
            {
                Id = booking.Id,
                EventId = booking.EventId,
                EventTitle = ev.Title,
                BookedAt = booking.BookedAt
            };
        }

        public async Task CancelBooking(Guid bookingId, Guid userId)
        {
            var booking = await _bkr.GetById(bookingId);

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.UserId != userId)
                throw new Exception("You cannot cancel this booking");

            await _bkr.CancelBooking(bookingId);
        }

        public async Task<List<BookingResponseDto>> GetRegistrationsByEvent(Guid eventId)
        {
            var bookings = await _bkr.GetByEventId(eventId);

            return bookings.Select(b => new BookingResponseDto
            {
                Id = b.Id,
                EventId = b.EventId,
                EventTitle = b.Event.Title,
                BookedAt = b.BookedAt
            }).ToList();
        }

        public async Task<List<BookingResponseDto>> GetAllBookings()
        {
            var bookings = await _bkr.GetAllBookings();

            return bookings.Select(b => new BookingResponseDto
            {
                Id = b.Id,
                EventId = b.EventId,
                EventTitle = b.Event.Title,
                BookedAt = b.BookedAt
            }).ToList();
        }
    }
       
}
