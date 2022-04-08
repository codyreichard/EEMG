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
        public IActionResult AddEvent(string eventTitle, DateTime eventTime, IFormFile eventFile) {
            Events newEvent = new Events();
            newEvent.EventTitle = eventTitle;
            newEvent.EventDate = eventTime;
            newEvent.FileName = eventFile.FileName;

            if (eventFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    eventFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    newEvent.FileContents = fileBytes;
                }
            }


            _context.Events.Add(newEvent);
            _context.SaveChanges();



            return RedirectToAction("Index", "Admin");
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
            EventSignUpModel model = new EventSignUpModel(id);
            return View("../EventSignUp", model);
        }

        [HttpPost]
        public IActionResult SignupUserForEvent(int eventId, string firstName, string lastName, string organization, string email)
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

            EventDetailsModel model = new EventDetailsModel(_context, true);

            HttpContext.Session.SetString("user_signed_up", "true");

            //model.UserSignedUp = true;
            return RedirectToPage("/EventDetails", model);
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

        // GET: Events
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Events.ToListAsync());
        //}

        //// GET: Events/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var events = await _context.Events
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (events == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(events);
        //}

        //// GET: Events/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Events/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,EventDate,EventTitle,FileName,FileContents")] Events events)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(events);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(events);
        //}

        //// GET: Events/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var events = await _context.Events.FindAsync(id);
        //    if (events == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(events);
        //}

        //// POST: Events/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,EventDate,EventTitle,FileName,FileContents")] Events events)
        //{
        //    if (id != events.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(events);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EventsExists(events.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(events);
        //}

        //// GET: Events/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var events = await _context.Events
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (events == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(events);
        //}

        //// POST: Events/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var events = await _context.Events.FindAsync(id);
        //    _context.Events.Remove(events);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool EventsExists(int id)
        //{
        //    return _context.Events.Any(e => e.Id == id);
        //}
    }
}
