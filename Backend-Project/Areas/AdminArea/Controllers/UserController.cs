﻿using Backend_Project.Models;
using Backend_Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(string search)
        {
            var users = search != null ?
                _userManager.Users
                .Where(u => u.FullName.ToLower().Contains(search.ToLower()))
                .ToList() :
                _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Detail(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);
            return View(new UserDetailVM
            {
                User = user,
                UserRoles = userRoles
            });
        }

        public IActionResult BlockedUser()
        {
            return View(_userManager.Users.Where(u => !u.IsActive));
        }

        public async Task<IActionResult> EditRole(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(new ChangeRoleVM
            {
                User = user,
                UserRoles = await _userManager.GetRolesAsync(user),
                AllRoles = _roleManager.Roles.ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string id, List<string> roles)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);

            await _userManager.AddToRolesAsync(user, roles);


            return RedirectToAction("index", "user");
        }


    }
}

