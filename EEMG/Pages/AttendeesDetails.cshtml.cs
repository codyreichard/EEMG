using System.Collections.Generic;
using EEMG.Data;
using EEMG.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace EEMG.Pages
{
    public class AttendeesDetailsModel : PageModel
    {
        public List<UserEventSignUp> UserSignUps { get; set; }
        public bool IsFromAdmin { get; set; }

        public AttendeesDetailsModel(ApplicationDbContext context, bool isFromAdmin = true)
        {
            UserSignUps = context.EventUserSignUps.ToList();
            IsFromAdmin = isFromAdmin;
        }
    }
}
