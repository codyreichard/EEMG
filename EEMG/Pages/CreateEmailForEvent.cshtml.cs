using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEMG.Data;
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
        
        public string LatestEventUrl { get; set; }

        public CreateEmailForEventModel(ApplicationDbContext db) 
        {
            var latestEvent = db.Events.OrderBy(e => e.EventDate).FirstOrDefault();
            if (latestEvent != null)
            {
                LatestEventUrl = "https://localhost:44351/Events/EventSignUp/" + latestEvent.Id;
            }
        }
        public void OnGet()
        {
        }
    }
}
