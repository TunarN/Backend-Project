using System.ComponentModel.DataAnnotations;

namespace Backend_Project.ViewModels
{
    public class CategoryCreateVM
    {
        [Required, MaxLength(50)]
        public string CategoryName { get; set; }

        [Required, MinLength(5)]

        public string CategoryDescription { get; set; }
    }
}
