using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Application.DTOs.BookingDto
{
    public class CreateBookingDto
    {
        [Required]
        public Guid EventId { get; set; }
    }
}
