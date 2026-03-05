using Event_Management_System.Application.DTOs.UserDto;

namespace Event_Management_System.Application.Interface
{
    public interface IAuthService
    {
        Task<UserResponceDto> Register(RegisterUserDto dto);

        Task<string> Login(string email, string password);
    }
}
