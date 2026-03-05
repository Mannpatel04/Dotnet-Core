using System.ComponentModel.DataAnnotations;
using static Event_Management_System.Domain.Enums.Role;

namespace Event_Management_System.Application.DTOs.UserDto
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}
