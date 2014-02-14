using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TribunalRecords
{
    public class Case
    {
        public UInt32 ID { get; set; }
        public byte Games { get; set; }
        public byte Reports { get; set; }
        public byte Ally { get; set; }
        public byte Enemy { get; set; }
        public byte OFFENSIVE_LANGUAGE { get; set; }
        public byte VERBAL_ABUSE { get; set; }
        public byte INTENTIONAL_FEEDING { get; set; }
        public byte ASSISTING_ENEMY { get; set; }
        public byte UNSKILLED_PLAYER { get; set; }
        public byte NO_COMMUNICATION_WITH_TEAM { get; set; }
        public byte LEAVING_AFK { get; set; }
        public byte NEGATIVE_ATTITUDE { get; set; }
        public byte INAPPROPRIATE_NAME { get; set; }
        public byte SPAMMING { get; set; }
        public string Decision { get; set; }
        public string Agreement { get; set; }
        public string Punishment { get; set; }
        public bool Decided { get; set; }
        public bool Fixed { get; set; }

        public Case(UInt32 ID)
        {
            this.ID = ID;
            this.Games = 0;
            this.OFFENSIVE_LANGUAGE = 0;
            this.VERBAL_ABUSE = 0;
            this.INTENTIONAL_FEEDING = 0;
            this.ASSISTING_ENEMY = 0;
            this.UNSKILLED_PLAYER = 0;
            this.NO_COMMUNICATION_WITH_TEAM = 0;
            this.LEAVING_AFK = 0;
            this.NEGATIVE_ATTITUDE = 0;
            this.INAPPROPRIATE_NAME = 0;
            this.SPAMMING = 0;
            this.Fixed = false;
        }
    }
}
