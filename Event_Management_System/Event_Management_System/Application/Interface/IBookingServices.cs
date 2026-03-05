using Event_Management_System.Application.DTOs.BookingDto;
using Event_Management_System.Application.DTOs.UserDto;
using Microsoft.AspNetCore.Identity.Data;

namespace Event_Management_System.Application.Interface
{
    public interface IBookingServices
    {
        Task<BookingResponseDto> RegisterEvent(Guid userId, CreateBookingDto dto);
        Task CancelBooking(Guid bookingId, Guid userId);
        Task<List<BookingResponseDto>> GetAllBookings();
        Task<List<BookingResponseDto>> GetRegistrationsByEvent(Guid eventId);
    }
}
