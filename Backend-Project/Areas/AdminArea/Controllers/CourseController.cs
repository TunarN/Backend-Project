using Backend_Project.DAL;
using Backend_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Courses.Include(c=>c.Category)
                .ToList());
        }

        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            Course course = _appDbContext.Courses.Include(c=>c.Category).SingleOrDefault(c => c.Id == id);
            if (course == null) return NotFound();
            return View(course);
        }
    }
}
