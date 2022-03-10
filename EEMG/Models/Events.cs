using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EEMG_Login.Data
{
    public class Events
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }

        public string EventTitle { get; set; }

        public string FileName { get; set; }
        public byte[] FileContents { get; set; }

        public Events() { }
    }
}
