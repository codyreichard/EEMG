using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EEMG.Pages
{
    public class CreateEmailForEventModel : PageModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool MemberOnly { get; set; }
        public bool EventParticipantsOnly { get; set; }
        public bool EntireMailingList { get; set; }

        public CreateEmailForEventModel() { }
        public void OnGet()
        {
        }
    }
}
