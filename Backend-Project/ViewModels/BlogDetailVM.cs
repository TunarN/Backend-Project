using Backend_Project.Models;

namespace Backend_Project.ViewModels
{
    public class BlogDetailVM
    {
        public string ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDesc { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> CategoryNames { get; set; }
        public Blog Blog { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
