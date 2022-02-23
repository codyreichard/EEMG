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
    public class ShowUsersModel : PageModel
    {
        private ApplicationDbContext _db;
        public List<IdentityUser> Users { get; set; }

        public ShowUsersModel(ApplicationDbContext db)
        {
            _db = db;
            this.Users = _db.Users.ToList();
        }
    }
}
