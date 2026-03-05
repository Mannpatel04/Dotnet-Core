namespace Event_Management_System.Application.DTOs.EventDto
{
    public class EventResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int capacity { get; set; }
       // public string OrganizerName { get; set; }
    }
}
