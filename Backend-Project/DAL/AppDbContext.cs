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

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }


    }
}
