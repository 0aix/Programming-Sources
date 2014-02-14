using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TribunalRecords
{
    public class Record
    {
        public string Champion { get; set; }
        public bool Team { get; set; }
        public bool ReportedTeam { get; set; }
        public bool Offender { get; set; }
        public byte Outcome { get; set; }
        public UInt16 Time { get; set; }
        public byte K { get; set; }
        public byte D { get; set; }
        public byte A { get; set; }
        public UInt16 G { get; set; }

        public Record()
        {

        }
    }
}
