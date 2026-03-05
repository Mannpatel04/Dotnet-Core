
using Event_Management_System.Application.Interface;
using Event_Management_System.Application.Services;
using Event_Management_System.Domain.Repository_Interface;
using Event_Management_System.Infrastructure.Data;
using Event_Management_System.Infrastructure.Repositories;
using Event_Management_System.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace Event_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IUserRepository, UserRepositories>();
            builder.Services.AddScoped<IEventRepository, EventRepositories>();
            builder.Services.AddScoped<IBookingRepository, BookingRepositories>();

            builder.Services.AddScoped<IEventServices,EventServices >();
            builder.Services.AddScoped<IBookingServices, BookingServices>();
            builder.Services.AddScoped<IAuthService, AuthServices>();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=> 
            {
                var key = builder.Configuration["Jwt:Key"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            builder.Services.AddRateLimiter(options=>
            {
                options.AddFixedWindowLimiter("fixed", config=>
                {
                    config.PermitLimit = 10;
                    config.Window = TimeSpan.FromMinutes(1);
                    config.QueueLimit = 0;
                });
            });


            builder.Services.AddControllers();
            
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
                app.MapGet("/", () => Results.Redirect("/scalar/v1"));  
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            app.UseRateLimiter();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
