using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EEMG
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }

        public string EventTitle { get; set; }

        public string FileName { get; set; }
        public byte[] FileContents { get; set; }
        public string Speaker { get; set; }
        public string SpeakerBio { get; set; } = ""; // needs to be set or will throw a Null Exception
        public string EventLocation { get; set; }
        public bool Archived { get; set; }

        public Event() { }
    }
}
