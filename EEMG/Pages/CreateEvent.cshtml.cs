using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public CreateEventModel() { }
    }
}
