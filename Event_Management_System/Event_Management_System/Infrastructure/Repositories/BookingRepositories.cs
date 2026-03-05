using Event_Management_System.Application.DTOs.BookingDto;
using Event_Management_System.Domain.Model;
using Event_Management_System.Domain.Repository_Interface;
using Event_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Infrastructure.Repositories
{
    public class BookingRepositories : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepositories(AppDbContext context)
        {
            _context = context;

        }
        
        public async Task<Booking> GetById(Guid id)
        {
            return await _context.Bookings.Include(x => x.Event).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> IsExistEvent(Guid EventId, Guid UserId)
        {
            return await _context.Bookings.AnyAsync(x => x.EventId == EventId && x.UserId == UserId);

        }
        public async Task<int> CountByEventId(Guid EventId)
        {
            return await _context.Bookings.CountAsync(x => x.EventId == EventId);

        }
        public async Task Addbooking(Booking booking)
        {

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

        }
        public async Task<bool> CancelBooking(Guid bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            if (booking == null)
            {
                return false;
            }
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;    
        }
        public async Task<List<Booking>> GetByEventId(Guid eventId)
        {
            return await _context.Bookings
                .Include(x => x.Event)
                .Where(x => x.EventId == eventId)
                .ToListAsync();
        }
        public async Task<List<Booking>> GetAllBookings()
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .ToListAsync();
        }
    }
}
