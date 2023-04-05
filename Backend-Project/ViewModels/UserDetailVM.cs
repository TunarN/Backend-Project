using Backend_Project.Models;

namespace Backend_Project.ViewModels
{
    public class UserDetailVM
    {

        public AppUser User { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
