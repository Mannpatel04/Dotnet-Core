namespace Event_Management_System.Domain.Model
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid EventId {get; set;}
        public Event Event { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public DateTime BookedAt { get; set;}
       
    }
}
