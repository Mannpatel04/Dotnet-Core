using Event_Management_System.Application.DTOs.UserDto;
using Event_Management_System.Domain.Model;
using Event_Management_System.Domain.Repository_Interface;
using Event_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Infrastructure.Repositories
{
    public class UserRepositories : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepositories(AppDbContext context)
        {
            _context  = context;
        }
        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<User> GetByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(x=> x.Email == Email);
        }


    }
}
