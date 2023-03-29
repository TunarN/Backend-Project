using Backend_Project.DAL;
using Backend_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public TeacherController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            List<Teacher> teachers = _appDbContext.Teachers.ToList();
            return View(teachers);
        }
    }
}
