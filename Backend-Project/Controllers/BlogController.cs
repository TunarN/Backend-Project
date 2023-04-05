﻿using Backend_Project.DAL;
using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        public BlogController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Blog> blogs = _appDbContext.Blogs.ToList();
            return View(blogs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.UserId = null;

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserId = user.Id;
            }
            if (id == null) return NotFound();
            Blog blog = _appDbContext.Blogs.Include(b => b.Comments).FirstOrDefault(b => b.Id == id);
            var category = _appDbContext.Categories.ToList();
            if (blog == null) return NotFound();
            BlogDetailVM blogDetailVM = new();
            blogDetailVM.ImageUrl = blog.ImageUrl;
            blogDetailVM.DateTime = blog.DateTime;
            blogDetailVM.BlogTitle = blog.BlogTitle;
            blogDetailVM.BlogDesc = blog.BlogDesc;
            blogDetailVM.Blog = blog;

            List<string> categoryNames = new();
            foreach (var item in category)
            {
                categoryNames.Add(item.CategoryName);
            }
            blogDetailVM.CategoryNames = categoryNames;
            return View(blogDetailVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string commentMessage, int blogId)
        {
            AppUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("login", "account");
            }
            Comment comment = new();
            comment.CreatedDate = DateTime.Now;
            comment.AppUserId = user.Id;
            comment.BlogId = blogId;
            comment.Message = commentMessage;
            _appDbContext.Comments.Add(comment);
            _appDbContext.SaveChanges();
            return RedirectToAction("detail", new { id = blogId });
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = _appDbContext.Comments.FirstOrDefault(b => b.Id == id);
            _appDbContext.Comments.Remove(comment);
            _appDbContext.SaveChanges();
            return RedirectToAction("detail", new { id = comment.BlogId });
        }
    }
}
