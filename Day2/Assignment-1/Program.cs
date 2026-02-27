using Assignment_1.Data;
using Assignment_1.Interfaces;
using Assignment_1.Mapping;
using Assignment_1.Middleware;
using Assignment_1.Models;
using Assignment_1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
namespace Assignment_1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDBContext>();

            builder.Services.AddScoped<IProductService, ProductServices>();

            builder.Services.AddTransient<ITransientService, LifetimeService>();
            builder.Services.AddScoped<IScopedService, LifetimeService>();
            builder.Services.AddSingleton<ISingletonService, LifetimeService>();


            builder.Services.AddAutoMapper(typeof(ProductProfile));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });


            builder.Services.AddAuthorization();
            var app = builder.Build();


          
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("My API");
                    options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
           

            app.UseMiddleware<RequestLoggingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
   