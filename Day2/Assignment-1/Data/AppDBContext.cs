using Assignment_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = MANN\\SQLEXPRESS; Database=DotNetCore;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}







