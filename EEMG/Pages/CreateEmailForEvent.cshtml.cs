using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EEMG.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public List<SelectListItem> EventsSelectList { get; set; }
        public int SelectedEventId { get; set; }

        public CreateEmailForEventModel(ApplicationDbContext db) 
        {
            var latestEvent = db.Events.OrderBy(e => e.EventDate).FirstOrDefault();
            if (latestEvent != null)
            {
                LatestEventUrl = "https://localhost:44351/Events/EventSignUp/" + latestEvent.Id;
            }

            EventsSelectList = new List<SelectListItem>();
            var events = db.Events.ToList();
            foreach(var eve in events)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = eve.EventTitle,
                    Value = eve.Id.ToString()
                };
                EventsSelectList.Add(selectListItem);
            }
        }
        public void OnGet()
        {
        }
    }
}
