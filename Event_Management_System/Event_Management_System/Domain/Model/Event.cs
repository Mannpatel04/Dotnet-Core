namespace Event_Management_System.Domain.Model
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int capacity { get; set; }

        public Guid OrganizerId { get; set; }
        public User Organizer { get; set; }

        public ICollection<Booking> Registrations { get; set; } 
    }
}
