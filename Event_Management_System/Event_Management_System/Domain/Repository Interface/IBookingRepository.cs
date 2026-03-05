using Event_Management_System.Application.DTOs.BookingDto;
using Event_Management_System.Domain.Model;

namespace Event_Management_System.Domain.Repository_Interface
{
    public interface IBookingRepository
    {
        Task<Booking> GetById(Guid id);
        Task<bool>IsExistEvent(Guid EventId, Guid UserId);
        Task<int> CountByEventId(Guid EventId);
        Task Addbooking(Booking booking);
        Task<bool> CancelBooking(Guid bookingId);
        Task<List<Booking>> GetByEventId(Guid eventId);
        Task<List<Booking>> GetAllBookings();
        Task<List<BookingResponseDto>> GetRegistrationByOrganizerId(Guid OrganizerId);



    }
}