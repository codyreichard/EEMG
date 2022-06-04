using System;
using EEMG.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace EEMG.Pages
{
    public class CreateEventModel : PageModel
    {
        public string EventTitle { get; set; }
        public DateTime EventTime { get; set; }
        //public string FileName { get; set; }
        //public byte[] FileContents { get; set; }
        public IFormFile EventFile { get; set; }
        public string Speaker { get; set; }
        public string SpeakerBio { get; set; }
        public string EventLocation { get; set; }
        public int Id { get; set; }


        private ApplicationDbContext db;
        //public CreateEventModel() { }

        public CreateEventModel(ApplicationDbContext db, int id = -1)
        {
            this.db = db;
            if (id > 0)
            {
                var eve = db.Events.FirstOrDefault(e => e.Id == id);

                EventTitle = eve.EventTitle;
                EventTime = eve.EventDate;
                Speaker = eve.Speaker;
                SpeakerBio = eve.SpeakerBio;
                EventLocation = eve.EventLocation;
                Id = id;
            }
        }
    }
}
