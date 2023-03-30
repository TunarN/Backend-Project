using Backend_Project.Models;

namespace Backend_Project.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Notice> Notices { get; set; }
        public List<EduHome> EduHomes { get; set; }
        public List<Course> Courses { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
