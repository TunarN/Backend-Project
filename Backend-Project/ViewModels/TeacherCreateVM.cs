namespace Backend_Project.ViewModels
{
    public class TeacherCreateVM
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Pinterest { get; set; }
        public string Vimeo { get; set; }
        public string Twitter { get; set; }
        public string AboutMe { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Hobbies { get; set; }
        public string Faculty { get; set; }
        public int ContactId { get; set; }

        public int SkillsId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
