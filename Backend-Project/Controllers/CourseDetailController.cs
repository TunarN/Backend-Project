using Backend_Project.DAL;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project.Controllers
{
    public class CourseDetailController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CourseDetailController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index(int id)
        {
            if (id == null) return NotFound();
            var course = _appDbContext.Courses.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            var category = _appDbContext.Categories.ToList();
            if (course == null) return NotFound();
            CourseDetailVM courseDetailVM = new();
            courseDetailVM.Name = course.Name;
            courseDetailVM.ImageUrl = course.ImageUrl;
            courseDetailVM.NameDesc = course.NameDesc;
            courseDetailVM.StartDate= course.StartDate;
            courseDetailVM.Month= course.Month;
            courseDetailVM.DurationHour= course.DurationHour;
            courseDetailVM.SkillLevel= course.SkillLevel;
            courseDetailVM.Language= course.Language;
            courseDetailVM.StudentsCount= course.StudentsCount;
            courseDetailVM.Assesment= course.Assesment;
            courseDetailVM.Fee= course.Fee;
            courseDetailVM.CourseDesc= course.CourseDesc;
            courseDetailVM.AboutCourse= course.AboutCourse;
            courseDetailVM.Apply= course.Apply;
            courseDetailVM.Certification= course.Certification;


            List<string> categoryNames = new();
            foreach (var item in category)
            {
                categoryNames.Add(item.CategoryName);
            }
            courseDetailVM.CategoryNames = categoryNames;
            return View(courseDetailVM);
        }
    }
}
