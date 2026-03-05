using Event_Management_System.Domain.Model;

namespace Event_Management_System.Application.DTOs.BookingDto
{
    public class BookingResponseDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime BookedAt { get; set; }
    }
}
