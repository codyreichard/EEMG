using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EEMG_Login.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EEMG_Login.Pages
{
    public class AdminModel : PageModel
    {
        private ApplicationDbContext _db;
        public List<User> Users;

        
        public AdminModel(ApplicationDbContext db)
        {
            _db = db;
            Users = new List<User>();
            var dbUsers = _db.Users.ToList();
            foreach (var user in dbUsers)
            {
                var role = _db.UserRoles.Where(e => e.UserId == user.Id).First();
                User u = new User();
                u.UserName = user.UserName;
                u.UserId = user.Id;
                u.RoleId = role.RoleId;
                u.RoleName = _db.Roles.Where(e => e.Id == role.RoleId).FirstOrDefault().Name;
                Users.Add(u);
            }

        }


        //public bool IsMember(IdentityUser user)
        //{
        //    _db.UserRoles.Where(e => e.UserId == user.UserName);
        //    return true;
        //}
    }

    public class User
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
