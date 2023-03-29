using Backend_Project.DAL;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project.Controllers
{
    public class TeacherDetailController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public TeacherDetailController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index(int id)
        {
            if(id==null) return NotFound();
            var teacher = _appDbContext.Teachers.Include(t=>t.Contact).Include(t=>t.Skills).FirstOrDefault(t=>t.Id==id);
            if(teacher==null) return NotFound();
            TeacherDetailVM teacherDetailVM = new();
            teacherDetailVM.Name= teacher.Name;
            teacherDetailVM.ImageUrl= teacher.ImageUrl;
            teacherDetailVM.Description= teacher.Description;
            teacherDetailVM.Facebook= teacher.Facebook;
            teacherDetailVM.Twitter = teacher.Twitter;
            teacherDetailVM.Pinterest= teacher.Pinterest;
            teacherDetailVM.Vimeo= teacher.Vimeo;
            teacherDetailVM.AboutMe= teacher.AboutMe;
            teacherDetailVM.Degree= teacher.Degree;
            teacherDetailVM.Experience= teacher.Experience;
            teacherDetailVM.Hobbies= teacher.Hobbies;
            teacherDetailVM.Faculty= teacher.Faculty;
            teacherDetailVM.Mail=teacher.Contact.Mail;
            teacherDetailVM.PhoneNumber= teacher.Contact.PhoneNumber;
            teacherDetailVM.Skype= teacher.Contact.Skype;
            teacherDetailVM.PercentComminication = teacher.Skills.PercentComminication;
            teacherDetailVM.PercentLanguage= teacher.Skills.PercentLanguage;
            teacherDetailVM.PercentDevelopment= teacher.Skills.PercentDevelopment;
            teacherDetailVM.PercentTeamLeader= teacher.Skills.PercentTeamLeader;
            teacherDetailVM.PercentDesign= teacher.Skills.PercentDesign;
            teacherDetailVM.PercentInnovation=teacher.Skills.PercentInnovation;

            return View(teacherDetailVM);
        }
    }
}
