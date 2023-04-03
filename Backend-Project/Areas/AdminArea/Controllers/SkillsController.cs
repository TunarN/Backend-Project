using Backend_Project.DAL;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SkillsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public SkillsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Skills.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SkillsCreateVM skills)
        {
            if (!ModelState.IsValid) return View();
            Skills newSkills = new()
            {
                PercentLanguage = skills.PercentLanguage,
                PercentComminication = skills.PercentComminication,
                PercentInnovation= skills.PercentInnovation,
                PercentDesign=skills.PercentDesign,
                PercentDevelopment=skills.PercentDevelopment,
                PercentTeamLeader=skills.PercentTeamLeader,
            };
            _appDbContext.Skills.Add(newSkills);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            Skills skills = _appDbContext.Skills.SingleOrDefault(c => c.Id == id);
            if (skills == null) return NotFound();
            return View(skills);
        }



        public IActionResult Edit(int id)
        {
            if (id == null) return NotFound();
            Skills skills = _appDbContext.Skills.SingleOrDefault(c => c.Id == id);
            if (skills == null) return NotFound();
            return View(new SkillUpdateVM { PercentLanguage = skills.PercentLanguage, PercentComminication = skills.PercentComminication,PercentDevelopment=skills.PercentDevelopment,PercentDesign=skills.PercentDesign,PercentInnovation=skills.PercentInnovation,PercentTeamLeader=skills.PercentTeamLeader });
        }

        [HttpPost]
        public IActionResult Edit(int id, SkillUpdateVM updateVM)
        {
            if (id == null) return NotFound();
            Skills existSkills = _appDbContext.Skills.Find(id);
            if (!ModelState.IsValid) return View();
            if (existSkills == null) return NotFound();
            existSkills.PercentLanguage = updateVM.PercentLanguage;
            existSkills.PercentDevelopment = updateVM.PercentDevelopment;
            existSkills.PercentTeamLeader  = updateVM.PercentTeamLeader;
            existSkills.PercentDesign=updateVM.PercentDesign;
            existSkills.PercentComminication = updateVM.PercentComminication;
            existSkills.PercentInnovation = updateVM.PercentInnovation;
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();
            Skills skills = _appDbContext.Skills.SingleOrDefault(c => c.Id == id);
            if (skills == null) return NotFound();
            _appDbContext.Skills.Remove(skills);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
