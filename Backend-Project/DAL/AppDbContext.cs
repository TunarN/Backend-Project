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
        public DbSet<Notice> Notices { get; set; }
        public DbSet<EduHome> EduHomes { get; set; }

        public DbSet<Bio> Bios { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
