using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EEMG.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EEMG.Pages
{
    public class EventDetailsModel : PageModel
    {
        private readonly EEMG.Data.ApplicationDbContext _context;
        public List<Event> Events { get; set; }
        public List<UserEventSignUp> UserEventSignUps { get; set; }

        public bool UserSignedUp { get; set; }
        public bool UpcomingStarted { get; set; } = false;
        public EventDetailsModel(Data.ApplicationDbContext context, bool userSignedUp = false)
        {
            _context = context;


            //we dont want to show events that are older than a year
            var lastYearOfShownEvent = DateTime.Now.Year - 2;

            Events = _context.Events.Where(e => e.Archived != true && e.EventDate.Year > lastYearOfShownEvent).OrderBy(e => e.EventDate).ToList();
            UserEventSignUps = _context.EventUserSignUps.ToList();
        }


        public void OnGet()
        {
            //first check the session storage, this will be used if the user hasnt created an account yet 
            var sUserSignedUp = HttpContext.Session.GetString("user_signed_up") ?? "";
            if (bool.TryParse(sUserSignedUp, out bool signedUp))
                UserSignedUp = signedUp;

            try
            {
                //if they are logged in see if the event 
                if (HttpContext != null && HttpContext.User != null)
                {
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var user = _context.ApplicationUsers.FirstOrDefault(e => e.Id == userId);

                    var userSignedUp = _context.EventUserSignUps.FirstOrDefault(e => e.Email.Equals(user.Email));

                    if (userSignedUp != null)
                        UserSignedUp = true;
                }
            }
            catch (Exception) { UserSignedUp = false; }

        }

        public bool CheckIfUserSignedUp(int eventId)
        {
            var sUserSignedUp = HttpContext.Session.GetString("user_signed_up") ?? "";
            var sessEventId = HttpContext.Session.GetInt32("sign_up_id");
            if (bool.TryParse(sUserSignedUp, out bool signedUp))
               return eventId == sessEventId ? signedUp : false;

            return false;
        }
    }
}
