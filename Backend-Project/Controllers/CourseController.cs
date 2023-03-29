using Microsoft.AspNetCore.Mvc;

namespace Backend_Project.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
