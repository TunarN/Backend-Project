using Backend_Project.DAL;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View(_appDbContext.Categories.ToList());
        }
        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _appDbContext.Categories.Any(c => c.CategoryName.ToLower() == category.CategoryName.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "bu adli categori movcuddur");
                return View();
            }
            Category newCategory = new()
            {
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription
            };
            _appDbContext.Categories.Add(newCategory);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id == null) return NotFound();
            Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(new CategoryUpdateVM { Name = category.CategoryName, Description = category.CategoryDescription });
        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryUpdateVM updateVM)
        {
            if (id == null) return NotFound();
            Category existCategory = _appDbContext.Categories.Find(id);
            if (!ModelState.IsValid) return View();
            bool isExist = _appDbContext.Categories.Any(c => c.CategoryName.ToLower() == updateVM.Name.ToLower() && c.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "bu adli categori movcuddur");
                return View();
            }
            if (existCategory == null) return NotFound();
            existCategory.CategoryDescription = updateVM.Description;
            existCategory.CategoryName = updateVM.Name;
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id == null) return NotFound();
            Category category = _appDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
