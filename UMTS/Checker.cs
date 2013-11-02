using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace UMTS
{
    public partial class Checker : Form
    {
        public static CookieContainer cc = new CookieContainer();
        public static string round = "";
        public static string eid = "";
        public static bool ready = true;

        public Checker()
        {
            InitializeComponent();
        }

        private string Access(string hc)
        {
            try
            {
                ready = false;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + round + "/game.php?&cmd=market&show=&hc=" + hc);
                request.CookieContainer = cc;
                request.Method = "POST";
                string postData = "";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                ready = true;
                return responseFromServer;
            }
            catch (Exception z)
            {
                return "bot_checker";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (Access("1").Contains("bot_checker"))
            {

            }
            else
            {
                this.Close();
            }
        }

        private void Checker_FormClosed(object sender, FormClosedEventArgs e)
        {
            UMTS.botcheck = false;
            eid = "";
        }

        private void CheckMacro_Tick(object sender, EventArgs e)
        {
            if(ready)
                label1_Click(null, null);
        }

        private void Checker_FormClosing(object sender, FormClosingEventArgs e)
        {
            UMTS.botcheck = false;
            eid = "";
        }

        private void Checker_Load(object sender, EventArgs e)
        {
            if (UMTS.invis = true)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
            }
        }
    }
}
