using Event_Management_System.Application.DTOs.EventDto;
using Event_Management_System.Application.DTOs.UserDto;
using Event_Management_System.Domain.Model;

namespace Event_Management_System.Domain.Repository_Interface
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string Email);
    }
}
