using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Application.DTOs.EventDto
{
    public class EventCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [Range(0, 100000)]
        public int capacity { get; set; }
    }
}
