using Event_Management_System.Application.DTOs.EventDto;
using Event_Management_System.Application.Interface;
using Event_Management_System.Domain.Model;
using Event_Management_System.Domain.Repository_Interface;

namespace Event_Management_System.Application.Services
{
   public class EventServices  : IEventServices
    {
        private readonly IEventRepository _evr;
        public EventServices(IEventRepository evr)
        {
            _evr = evr;
        }
        public async Task<EventResponseDto> CreateEvent(EventCreateDto dto, Guid organizerId)
        {
            var ev = new Event
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                EventDate = dto.EventDate,
                Location = dto.Location,
                capacity = dto.capacity,
                OrganizerId = organizerId
            };

            await _evr.CreateEvent(ev);

            return new EventResponseDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                EventDate = ev.EventDate,
                Location = ev.Location,
                capacity = ev.capacity
            };

        }
        public async Task<EventResponseDto> UpdateEvent(Guid eventId, EventUpdateDto dto, Guid userId)
        {
            var ev = await _evr.GetById(eventId);

            if (ev == null)
                throw new Exception("Event not found");

            if (ev.OrganizerId != userId)
                throw new Exception("You cannot update this event");

            //ev.Title = dto.Title;
            //ev.Description = dto.Description;
            ev.EventDate = dto.EventDate;
            ev.Location = dto.Location;
            //ev.capacity = dto.capacity;

            await _evr.UpdateEvent(ev);

            return new EventResponseDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                EventDate = ev.EventDate,
                Location = ev.Location,
                capacity = ev.capacity
            };
        }
        public async Task DeleteEvent(Guid eventId, Guid userId)
        {
            var ev = await _evr.GetById(eventId);

            if (ev == null)
                throw new Exception("Event not found");

            if (ev.OrganizerId != userId)
                throw new Exception("You are not allowed to delete this event");

            await _evr.DeleteEvent(eventId);
        }
        public async Task<List<EventResponseDto>> GetAllEvents()
        {
            var events = await _evr.GetAllEvents();
            return events.Select(ev=> new EventResponseDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                EventDate = ev.EventDate,
                Location = ev.Location,
                capacity = ev.capacity
            }).ToList();
        }
        public async Task<EventResponseDto> GetEventById(Guid id)
        {
            var ev = await _evr.GetById(id);

            if (ev == null)
                throw new Exception("Event not found");

            return new EventResponseDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                EventDate = ev.EventDate,
                Location = ev.Location,
                capacity = ev.capacity
            };
        }
        public async Task<List<EventResponseDto>> GetEventsByOrganizer(Guid organizerId)
        {
            var events = await _evr.GetByOrganizationId(organizerId);

            return events.Select(ev => new EventResponseDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                EventDate = ev.EventDate,
                Location = ev.Location,
                capacity = ev.capacity
            }).ToList();
        }


    }
}
