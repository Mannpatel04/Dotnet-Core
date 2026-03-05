using Event_Management_System.Application.DTOs.UserDto;
using Event_Management_System.Application.Interface;
using Event_Management_System.Domain.Model;
using Event_Management_System.Domain.Repository_Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Event_Management_System.Application.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IUserRepository _usr;
        private readonly IConfiguration _config;

        public AuthServices(IUserRepository usr, IConfiguration config)
        {
            _usr = usr;
            _config = config;
        }


        public async Task<UserResponceDto> Register(RegisterUserDto dto)
        {
            var existingUser = await _usr.GetByEmail(dto.Email);

            if (existingUser != null)
                throw new Exception("User already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Role = dto.Role
            };

            await _usr.AddUser(user);

            return new UserResponceDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usr.GetByEmail(email);

            if (user == null)
                throw new Exception("Invalid credentials");

            if (!VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(password);

            var hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

  
        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}   
