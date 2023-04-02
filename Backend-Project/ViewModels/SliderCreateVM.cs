using System.ComponentModel.DataAnnotations;

namespace Backend_Project.ViewModels
{
    public class SliderCreateVM
    {

        [Required]
        public IFormFile Photo { get; set; }
        [Required, MinLength(5)]
        public string Title { get; set; }
    }
}
