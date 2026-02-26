using Assignment_1.Data;
using Assignment_1.Interfaces;
using Assignment_1.Mapping;
using Assignment_1.Models;
using Assignment_1.Services;
using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(ProductProfile));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
   