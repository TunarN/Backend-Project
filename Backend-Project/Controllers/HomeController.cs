using Backend_Project.DAL;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Sliders = _appDbContext.Sliders.ToList();
            homeVM.Notices = _appDbContext.Notices.ToList();
            homeVM.EduHomes = _appDbContext.EduHomes.ToList();
            homeVM.Courses = _appDbContext.Courses.Take(3).ToList();
            homeVM.Blogs = _appDbContext.Blogs.Take(3).ToList();
            return View(homeVM);
        }



    }
}