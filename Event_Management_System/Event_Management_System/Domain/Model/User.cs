using static Event_Management_System.Domain.Enums.Role;

namespace Event_Management_System.Domain.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public RoleType Role { get; set; }

        public ICollection<Event> Events { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
