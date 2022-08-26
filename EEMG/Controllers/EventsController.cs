using System;
using Microsoft.AspNetCore.Mvc;
using EEMG.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using EEMG.Models;
using EEMG.Pages;

namespace EEMG.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddEvent(int id, string eventTitle, DateTime eventTime, IFormFile eventFile, string speaker, string speakerBio, string eventLocation) {

            Event newEvent;
            if (id <= 0)
                newEvent = new Event();
            else
                newEvent = _context.Events.FirstOrDefault(e => e.Id == id);

            newEvent.EventTitle = eventTitle;
            newEvent.EventDate = eventTime;
            newEvent.FileName = eventFile?.FileName;
            newEvent.Speaker = speaker;
            newEvent.SpeakerBio = speakerBio;
            newEvent.EventLocation = eventLocation;
            newEvent.Archived = false;

            if (eventFile != null && eventFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    eventFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    newEvent.FileContents = fileBytes;
                }
            }

            if (id <= 0)
                _context.Events.Add(newEvent);

            _context.SaveChanges();



            return RedirectToAction("Index", "Admin");
        }


        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            CreateEventModel model = new CreateEventModel(_context, id);
            return View("../CreateEvent", model);
        }

        [HttpGet]
        public FileContentResult DownloadFile(int id)
        {
            var eventDownload = _context.Events.First(e => e.Id == id);
            
            return File(eventDownload.FileContents, "application/vnd.openxmlformats-officedocument.presentationml.presentation", eventDownload.FileName);
        }

        [HttpGet]
        public IActionResult EventSignUp(int id)
        {
            EventSignUpModel model = new EventSignUpModel(_context, id);
            return View("../EventSignUp", model);
        }

        [HttpPost]
        public IActionResult SignupUserForEvent(int eventId, string firstName, string lastName, string organization, string email)
        {

            if (ModelState.IsValid)
            {
                var eventSignUps = _context.EventUserSignUps.ToList();


                var userSignedUp = _context.EventUserSignUps.Where(e => e.Email == email).ToList();


                if (userSignedUp.Count <= 0)
                {
                    UserEventSignUp newEventSignup = new UserEventSignUp();
                    newEventSignup.EventId = eventId;
                    newEventSignup.FirstName = firstName;
                    newEventSignup.LastName = lastName;
                    newEventSignup.Organization = organization;
                    newEventSignup.Email = email;
                    newEventSignup.AttendingEvent = true;
                    newEventSignup.PaidForEvent = false;

                    _context.EventUserSignUps.Add(newEventSignup);
                    _context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Email", "That email has already been signed up for the event.");
                    EventDetailsModel modelFailed = new EventDetailsModel(_context, false);
                    return View("../EventSignUp");
                }

                EventDetailsModel model = new EventDetailsModel(_context, true);
                HttpContext.Session.SetString("user_signed_up", "true");
                HttpContext.Session.SetInt32("sign_up_id", eventId);
                return RedirectToPage("/EventDetails", model);
            }

            return View("../EventDetails");
        }

        [HttpGet]
        public IActionResult ShowUsersAtEvent()
        {
            AttendeesDetailsModel model = new AttendeesDetailsModel(_context, false);
            return View("../AttendeesDetails", model);
        }

        [HttpGet]
        public IActionResult ShowUsersAtEventFromAdmin()
        {
            AttendeesDetailsModel model = new AttendeesDetailsModel(_context, true);
            return View("../AttendeesDetails", model);
        }
    }
}
