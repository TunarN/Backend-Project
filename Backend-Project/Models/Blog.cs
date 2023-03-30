namespace Backend_Project.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDesc { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
