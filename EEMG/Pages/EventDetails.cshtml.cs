using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EEMG_Login.Data;

namespace EEMG_Login.Pages
{
    public class EventDetailsModel : PageModel
    {
        private readonly EEMG_Login.Data.ApplicationDbContext _context;
        public List<Events> Events { get; set; }

        public EventDetailsModel(EEMG_Login.Data.ApplicationDbContext context)
        {
            _context = context;


            if (_context.Events.Count() <= 1)
            {
                Events events = new Events();
                events.EventDate = new DateTime(2021,10,1);
                events.EventTitle = "EEMG Luncheon OCT";
                events.FileName = "test.pptx";
                events.FileContents = System.IO.File.ReadAllBytes(@"../Presentation1.pptx");

                Events events1 = new Events();
                events1.EventDate = new DateTime(2021, 12, 1);
                events1.EventTitle = "EEMG Luncheon DEC";
                events1.FileName = "test.pptx";
                events1.FileContents = System.IO.File.ReadAllBytes(@"../Presentation1.pptx");

                _context.Events.AddRange(events, events1);


                _context.SaveChanges();
            }

            Events = _context.Events.ToList();

        }



    }
}
