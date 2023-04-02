using System.ComponentModel.DataAnnotations;

namespace Backend_Project.ViewModels
{
    public class SliderUpdtateVM
    {

        public string ImageUrl { get; set; }
        [Required, MinLength(5)]
        public string Title { get; set; }
        public IFormFile Photo { get; set; }
    }
}
