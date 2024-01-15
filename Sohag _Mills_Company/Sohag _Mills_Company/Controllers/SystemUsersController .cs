using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using System.Diagnostics;

namespace Sohag__Mills_Company.Controllers
{
    public class SystemUsersController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;

        public SystemUsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }


        public async Task<IActionResult> AllUsers()
        {
            var users = await userManager.Users.ToListAsync();
            ViewBag.Users = users;
            return View();
        }


    }
}