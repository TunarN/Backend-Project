using Backend_Project.DAL;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            List<Blog> blogs = _appDbContext.Blogs.ToList();
            return View(blogs);
        }

        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            var blog = _appDbContext.Blogs.Include(b => b.Comments).FirstOrDefault(b => b.Id == id);
            var category = _appDbContext.Categories.ToList();
            if (blog == null) return NotFound();
            BlogDetailVM blogDetailVM = new();
            blogDetailVM.ImageUrl= blog.ImageUrl;
            blogDetailVM.DateTime=blog.DateTime;
            blogDetailVM.BlogTitle=blog.BlogTitle;
            blogDetailVM.BlogDesc=blog.BlogDesc;
            List<string> categoryNames = new();
            foreach (var item in category)
            {
                categoryNames.Add(item.CategoryName);
            }
            blogDetailVM.CategoryNames = categoryNames;
            return View(blogDetailVM);
        }
    }
}
