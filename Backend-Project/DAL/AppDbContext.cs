using Backend_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }

    }
}
