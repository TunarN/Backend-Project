using Backend_Project.DAL;
using Backend_Project.Extension;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Blogs.ToList());
        }
        public IActionResult Detail(int id)
        {
            if (id == null) return NotFound();
            Blog blog = _appDbContext.Blogs.SingleOrDefault(c => c.Id == id);
            if (blog == null) return NotFound();
            return View(blog);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BlogCreateVM blogCreateVM)
        {
            if (blogCreateVM.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please Input Image");
                return View();

            }
            if (!blogCreateVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please input only Image");
                return View();
            }
            if (blogCreateVM.Photo.CheckSize(500))
            {
                ModelState.AddModelError("Photo", "Photo size is Large");
                return View();
            }

            Blog newBlog = new();
            newBlog.ImageUrl = blogCreateVM.Photo.SaveImage(_env, "img/blog", blogCreateVM.Photo.FileName);
            newBlog.BlogDesc = blogCreateVM.BlogDesc;
            newBlog.BlogTitle = blogCreateVM.BlogTitle;
            newBlog.DateTime = blogCreateVM.DateTime;

            _appDbContext.Blogs.Add(newBlog);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            var blog = _appDbContext.Blogs.SingleOrDefault(s => s.Id == id);
            if (blog == null) return NotFound();

            string fullPath = Path.Combine(_env.WebRootPath, "img/blog", blog.ImageUrl);
            if (System.IO.File.Exists(fullPath))
            {

                System.IO.File.Delete(fullPath);

            }

            _appDbContext.Blogs.Remove(blog);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id == null) return NotFound();
            Blog blog = _appDbContext.Blogs.SingleOrDefault(c => c.Id == id);
            if (blog == null) return NotFound();
            return View(new BlogUpdateVM { ImageUrl = blog.ImageUrl, BlogTitle = blog.BlogTitle, DateTime=blog.DateTime,BlogDesc=blog.BlogDesc });
        }

        [HttpPost]
        public IActionResult Edit(int id, BlogUpdateVM updateVM)
        {
            if (id == null) return NotFound();
            Blog blog = _appDbContext.Blogs.SingleOrDefault(c => c.Id == id);
            if (blog == null) return NotFound();
            if (updateVM.Photo != null)
            {
                if (!updateVM.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please input only Image");
                    return View();
                }
                if (updateVM.Photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Photo size is Large");
                    return View();
                }


                string fullPath = Path.Combine(_env.WebRootPath, "img/blog", blog.ImageUrl);
                if (System.IO.File.Exists(fullPath))
                {

                    System.IO.File.Delete(fullPath);

                }

                blog.ImageUrl = updateVM.Photo.SaveImage(_env, "img/blog", updateVM.Photo.FileName);
                blog.BlogTitle = updateVM.BlogTitle;
                blog.BlogDesc = updateVM.BlogDesc;
                blog.DateTime = updateVM.DateTime;
                _appDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

}

