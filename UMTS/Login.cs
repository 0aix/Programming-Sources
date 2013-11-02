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

namespace UMTS
{
    public partial class Login : Form
    {
        static CookieContainer ccc = new CookieContainer();

        private static string Loginx(string u, string p)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/login/");
            request.CookieContainer = ccc;
            request.Method = "POST";
            string postData = "username=" + u + "&password=" + p + "&Submit.x=19&Submit.y=12";
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
            return responseFromServer;
        }

        private static string roundx(string r)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round"+ r +"/game.php");
            request.CookieContainer = ccc;
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
            return responseFromServer;
        }

        private static string namex(string r)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.uwars.com/round" + r + "/game.php");
            request.CookieContainer = ccc;
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
            int i = responseFromServer.IndexOf("Round " + r + " - ") + ("Round " + r + " - ").Length;
            string name = "";
            while(true)
            {
                if (responseFromServer[i] != '[')
                {
                    name = name + responseFromServer[i];
                }
                else
                {
                    break;
                }
                i++;
            }
            return name;
        }

        public Login()
        {
            InitializeComponent();
        }

        private static bool IsNumeric(string s)
        {
            int i = 0;
            if (s.Length > 0)
            {
                while (i < s.Length)
                {
                    if (!char.IsNumber(s[i]))
                    {
                        return false;
                    }
                    i++;
                }
            }
            else
            {
                if (!char.IsNumber(s[0]))
                {
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (uid.TextLength == 0)
            {
                MessageBox.Show("Please enter your username.","Universal Market Trading System");
            }
            else if (pid.TextLength == 0)
            {
                MessageBox.Show("Please enter your password.", "Universal Market Trading System");
            }
            else if (rid.TextLength == 0)
            {
                MessageBox.Show("Please enter the round number.", "Universal Market Trading System");
            }
            else if (!IsNumeric(rid.Text))
            {
                MessageBox.Show("Please enter a NUMBER.", "Universal Market Trading System");
            }
            else
            {
                if (Loginx(uid.Text, pid.Text).Contains("Back") == false)
                {
                    if (roundx(rid.Text).Contains("Error") == false)
                    {
                        UMTS umts = new UMTS();
                        umts.Text += namex(rid.Text);
                        umts.Show();
                        UMTS.round = rid.Text;
                        UMTS.cc = ccc;
                        Log log = new Log();
                        log.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("The system could not log in. Please try again.", "Universal Market Trading System");
                    }
                }
                else
                {
                    MessageBox.Show("The system could not log in. Please try again.", "Universal Market Trading System");
                }
            }
        }

        private void uid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(null, null);
        }

        private void pid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(null, null);
        }

        private void rid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(null, null);
        }
    }
}
