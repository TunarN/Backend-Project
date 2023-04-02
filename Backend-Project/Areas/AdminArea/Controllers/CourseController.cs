using Backend_Project.DAL;
using Backend_Project.Extension;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(_appDbContext.Courses.Include(c => c.Category)
                .ToList());
        }

        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            Course course = _appDbContext.Courses.Include(c => c.Category).SingleOrDefault(c => c.Id == id);
            if (course == null) return NotFound();
            return View(course);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_appDbContext.Categories.ToList(), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CourseCreateVM courseCreateVM)
        {
            ViewBag.Categories = new SelectList(_appDbContext.Categories.ToList(), "Id", "CategoryName");

            if (courseCreateVM.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please Input Image");
                return View();

            }
            if (!courseCreateVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please input only Image");
                return View();
            }
            if (!courseCreateVM.Photo.CheckSize(50))
            {
                ModelState.AddModelError("Photo", "Photo size is Large");
                return View();
            }

            Course newcourse = new();
            newcourse.ImageUrl = courseCreateVM.Photo.SaveImage(_env, "img/course", courseCreateVM.Photo.FileName);
            newcourse.Name = courseCreateVM.Name;
            newcourse.NameDesc = courseCreateVM.NameDesc;
            newcourse.StartDate = courseCreateVM.StartDate;
            newcourse.Month = courseCreateVM.Month;
            newcourse.DurationHour = courseCreateVM.DurationHour;
            newcourse.SkillLevel = courseCreateVM.SkillLevel;
            newcourse.Language = courseCreateVM.Language;
            newcourse.Assesment = courseCreateVM.Assesment;
            newcourse.StudentsCount = courseCreateVM.StudentsCount;
            newcourse.Fee = courseCreateVM.Fee;
            newcourse.CourseDesc = courseCreateVM.CourseDesc;
            newcourse.AboutCourse = courseCreateVM.AboutCourse;
            newcourse.Apply = courseCreateVM.Apply;
            newcourse.Certification = courseCreateVM.Certification;
            newcourse.CategoryId = courseCreateVM.CategoryId;
            _appDbContext.Courses.Add(newcourse);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            var course = _appDbContext.Courses.SingleOrDefault(s => s.Id == id);
            if (course == null) return NotFound();

            string fullPath = Path.Combine(_env.WebRootPath, "img/course", course.ImageUrl);
            if (System.IO.File.Exists(fullPath))
            {

                System.IO.File.Delete(fullPath);

            }

            _appDbContext.Courses.Remove(course);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
