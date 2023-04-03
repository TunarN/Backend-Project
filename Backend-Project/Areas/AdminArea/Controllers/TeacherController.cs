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
    public class TeacherController : Controller
    {

        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Teachers.Include(t=>t.Contact)
               .ToList()
               );
        }

        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            Teacher teacher = _appDbContext.Teachers.Include(c => c.Contact).SingleOrDefault(c => c.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        public IActionResult Create()
        {
            ViewBag.Contact = new SelectList(_appDbContext.Contacts.ToList(), "Id", "Mail","PhoneNumber","Skype");
            ViewBag.Skills = new SelectList(_appDbContext.Skills.ToList(), "Id", "PercentLanguage");
            return View();
        }

        [HttpPost]
        public IActionResult Create(TeacherCreateVM teacherCreateVM)
        {
            ViewBag.Contact = new SelectList(_appDbContext.Categories.ToList(), "Id", "Mail", "PhoneNumber","Skype");
            ViewBag.Skills = new SelectList(_appDbContext.Skills.ToList(), "Id", "PercentLanguage");
            if (teacherCreateVM.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please Input Image");
                return View();

            }
            if (!teacherCreateVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please input only Image");
                return View();
            }
            if (!teacherCreateVM.Photo.CheckSize(50))
            {
                ModelState.AddModelError("Photo", "Photo size is Large");
                return View();
            }

            Teacher newteacher = new();
            newteacher.ImageUrl = teacherCreateVM.Photo.SaveImage(_env, "img/teacher", teacherCreateVM.Photo.FileName);
            newteacher.Name = teacherCreateVM.Name;
            newteacher.Description= teacherCreateVM.Description;
            newteacher.Facebook = teacherCreateVM.Facebook;
            newteacher.Pinterest= teacherCreateVM.Pinterest;
            newteacher.Vimeo= teacherCreateVM.Vimeo;
            newteacher.Twitter = teacherCreateVM.Twitter;
            newteacher.AboutMe= teacherCreateVM.AboutMe;
            newteacher.Degree= teacherCreateVM.Degree;
            newteacher.Experience= teacherCreateVM.Experience;
            newteacher.Hobbies= teacherCreateVM.Hobbies;
            newteacher.Faculty= teacherCreateVM.Faculty;
            newteacher.ContactId= teacherCreateVM.ContactId;
            newteacher.SkillsId = teacherCreateVM.SkillsId;
            _appDbContext.Teachers.Add(newteacher);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            var teacher = _appDbContext.Teachers.SingleOrDefault(s => s.Id == id);
            if (teacher == null) return NotFound();

            string fullPath = Path.Combine(_env.WebRootPath, "img/teacher", teacher.ImageUrl);
            if (System.IO.File.Exists(fullPath))
            {

                System.IO.File.Delete(fullPath);

            }

            _appDbContext.Teachers.Remove(teacher);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

