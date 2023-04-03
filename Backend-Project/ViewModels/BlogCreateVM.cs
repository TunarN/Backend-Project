using System.ComponentModel.DataAnnotations;

namespace Backend_Project.ViewModels
{
    public class BlogCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required, MinLength(5)]
        public string ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDesc { get; set; }
    }
}
