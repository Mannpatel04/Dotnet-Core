using Event_Management_System.Application.DTOs.EventDto;
using Event_Management_System.Domain.Model;

namespace Event_Management_System.Domain.Repository_Interface
{
    public interface IEventRepository
    {
       Task CreateEvent(Event ev);
       Task UpdateEvent (Event ev);
       Task DeleteEvent(Guid id);

       Task<Event> GetById(Guid id);
       Task<List<Event>> GetAllEvents();
       Task<List<Event>> GetByOrganizationId(Guid organizationId);

       
    }
}
