using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TribunalRecords
{
    public class Tribunal
    {
        public string Champion { get; set; }
        public bool Team { get; set; }
        public byte Outcome { get; set; }
        public UInt16 Time { get; set; }
        public string Report { get; set; }
        public byte OK { get; set; }
        public byte OD { get; set; }
        public byte OA { get; set; }
        public UInt16 OG { get; set; }
        public UInt16 TK { get; set; }
        public UInt16 TD { get; set; }
        public UInt16 TA { get; set; }
        public UInt32 TG { get; set; }

        public Tribunal()
        {

        }

        public void Record(Record recordData)
        {
            Champion = recordData.Champion;
            Team = recordData.Team;
            Outcome = recordData.Outcome;
            Time = recordData.Time;
            OK = recordData.K;
            OD = recordData.D;
            OA = recordData.A;
            OG = recordData.G;
        }

        public void AddKDA(Record recordData)
        {
            TK += (UInt16)recordData.K;
            TD += (UInt16)recordData.D;
            TA += (UInt16)recordData.A;
            TG += (UInt32)recordData.G;
        }
    }
}
