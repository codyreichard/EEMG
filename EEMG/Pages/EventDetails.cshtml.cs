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


            if (_context.Events.Count() <= 1)
            {
                Event events = new Event();
                events.EventDate = new DateTime(2021,10,1);
                events.EventTitle = "EEMG Luncheon OCT";
                events.FileName = "test.pptx";
                events.FileContents = System.IO.File.ReadAllBytes(@"../Presentation1.pptx");

                Event events1 = new Event();
                events1.EventDate = new DateTime(2023, 12, 1);
                events1.EventTitle = "EEMG Luncheon DEC";
                events1.FileName = "test.pptx";
                events1.FileContents = System.IO.File.ReadAllBytes(@"../Presentation1.pptx");

                _context.Events.AddRange(events, events1);


                _context.SaveChanges();
            }

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
            }catch(Exception) { }

        }
    }
}
