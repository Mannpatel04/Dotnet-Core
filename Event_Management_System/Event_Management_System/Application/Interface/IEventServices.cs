using Event_Management_System.Application.DTOs.BookingDto;
using Event_Management_System.Application.DTOs.EventDto;
using Event_Management_System.Domain.Enums;
using Event_Management_System.Domain.Model;

namespace Event_Management_System.Application.Interface
{
    public interface IEventServices
    {
        Task<EventResponseDto> CreateEvent(EventCreateDto dto, Guid organizerId);
        Task<EventResponseDto> UpdateEvent(Guid eventId, EventUpdateDto dto, Guid userId);
        Task DeleteEvent(Guid eventId, Guid userId);
        Task<List<EventResponseDto>> GetAllEvents();
        Task<EventResponseDto> GetEventById(Guid id);
        Task<List<EventResponseDto>> GetEventsByOrganizer(Guid organizerId);

    }
}
