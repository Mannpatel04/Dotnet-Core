using Event_Management_System.Application.DTOs.EventDto;
using Event_Management_System.Domain.Model;
using Event_Management_System.Domain.Repository_Interface;
using Event_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Infrastructure.Repositories
{
    public class EventRepositories : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepositories(AppDbContext context)
        {
            _context = context;
        }

        public  async Task CreateEvent(Event ev)
        {
            await _context.Events.AddAsync(ev);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEvent(Event ev)
        {
            _context.Events.Update(ev);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEvent(Guid id)
        {
            var findEvent = await  _context.Events.FindAsync(id);
            if (findEvent != null)
            {
                _context.Events.Remove(findEvent);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<Event> GetById(Guid id)
        {
            return await _context.Events.Include(x => x.Organizer).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Event>> GetAllEvents()
        {
            return await _context.Events.Include(x => x.Organizer).ToListAsync();
        }
        public async Task<List<Event>> GetByOrganizationId(Guid organizationId)
        {
            return await _context.Events.Where(x => x.OrganizerId == organizationId).Include(e => e.Organizer).ToListAsync();
        }

    }
}
