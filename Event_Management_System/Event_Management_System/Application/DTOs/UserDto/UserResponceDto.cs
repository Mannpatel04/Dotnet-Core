using static Event_Management_System.Domain.Enums.Role;

namespace Event_Management_System.Application.DTOs.UserDto
{
    public class UserResponceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
       public RoleType Role { get; set; }
    }
}
