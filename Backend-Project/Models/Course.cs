namespace Backend_Project.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameDesc { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public double Month { get; set; }
        public double DurationHour { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int StudentsCount { get; set; }
        public string Assesment { get; set; }
        public double Fee { get; set; }
        public string CourseDesc { get; set; }
        public string AboutCourse { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
