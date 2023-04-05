using Microsoft.AspNetCore.Identity;

namespace Backend_Project.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
