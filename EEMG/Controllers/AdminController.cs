using System;
using System.Linq;
using System.Threading.Tasks;
using EEMG.Data;
using EEMG.Models;
using EEMG.Pages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EEMG.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _db;
        private IServiceProvider _serviceProvider;

        public AdminController(ApplicationDbContext context, IServiceProvider provider)
        {
            _db = context;
            _serviceProvider = provider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            AdminModel model = new AdminModel(_db);
            return RedirectToAction("/Admin", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserToMember(string userId)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userRole = _db.UserRoles.FirstOrDefault(x => x.UserId == userId);
            var user = _db.ApplicationUsers.First(x => x.Id == userId);

            // THIS LINE IS IMPORTANT
            var oldUser = userManager.FindByIdAsync(userId);
            var oldRoleId = userRole.RoleId;
            var oldRoleName = _db.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

            if (_db.Roles.FirstOrDefault(x => x.Id == userRole.RoleId)?.Name != "Member")
            {
                await userManager.RemoveFromRoleAsync(user, oldRoleName);
                await userManager.AddToRoleAsync(user, "Member");
                _db.Entry(user).State = EntityState.Modified;
            }

            AdminModel model = new AdminModel(_db);
            return new OkObjectResult(new { success = true, message = "" });
        }

        [HttpGet]
        public IActionResult AddEvent()
        {
            return RedirectToAction("~/CreateEvent");
        }
    }
}
