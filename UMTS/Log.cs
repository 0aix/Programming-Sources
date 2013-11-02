using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UMTS
{
    public partial class Log : Form
    {
        public static string content = "";
        public static bool canclose = false;
        public static int profit = 0;

        public Log()
        {
            InitializeComponent();
        }

        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canclose)
            {
                e.Cancel = true;
                this.UMTS.Visible = true;
                this.UMTS.ShowBalloonTip(1000);
                Hide();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Logfile.Text = content;
            Logfile.SelectionStart = Logfile.TextLength;
            Logfile.ScrollToCaret();
            this.Text = "Transaction Log - [" + profit.ToString() + "]";
        }

        private void UMTS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.UMTS.Visible = false; 
        }
    }
}
