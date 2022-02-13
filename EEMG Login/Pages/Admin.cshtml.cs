using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEMG_Login.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EEMG_Login.Pages
{
    public class AdminModel : PageModel
    {
        private ApplicationDbContext _db;
        public List<IdentityUser> Users;

        public AdminModel(ApplicationDbContext db)
        {
            _db = db;
            Users = _db.Users.ToList();
        }


        public bool IsMember(IdentityUser user)
        {
            _db.UserRoles.Where(e => e.UserId == user.UserName);
            return true;
        }
    }
}
