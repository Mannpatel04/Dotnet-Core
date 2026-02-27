using Assignment_1.DTOs;
using Assignment_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static List<User> Users = new();
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config )
        {
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            Users.Add(new User
            {
                Id = Users.Count + 1,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            });

            return Ok("User registered");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = Users.FirstOrDefault(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }
                
            var token = GenerateToken(user);
            return Ok(new {token});
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials:
                    new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
