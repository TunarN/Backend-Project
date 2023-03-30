using Backend_Project.DAL;
using Backend_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CourseController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {

            List<Course> courses = _appDbContext.Courses.ToList();
            return View(courses);
        }
    }
}
